using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Dunder.Mifflin.Api.DB;
using Dunder.Mifflin.Api.Models.DB;
using Dunder.Mifflin.Api.Models.Domain;
using Dunder.Mifflin.Api.Settings;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Dunder.Mifflin.Api.Repositories.Impl;

[SuppressMessage("ReSharper", "RedundantAnonymousTypePropertyName")]
public class PostgreSqlRepository : IDbRepository
{
    private readonly string _connectionString;

    public PostgreSqlRepository(IOptions<ConnectionStrings> options)
    {
        _connectionString = options.Value.DunderMifflinPostgreSql;
    }

    public async Task SaveLinesFromCsvFileToDbIfDbEmpty()
    {
        await using NpgsqlConnection connection = new(_connectionString);

        var length = await connection.QueryFirstAsync<int>("select count(line_id) from line");
        if (length != 0)
            return;

        var lines = PersistentDataFetcher.GetLinesFromCsvFile().ToList();
        var size = lines.Count;

        Console.WriteLine("Adding lines to DB");

        foreach (var lineTuple in lines.Select((line, index) => new { index = index, line = line }))
        {
            var line = lineTuple.line;
            var index = lineTuple.index;

            await connection.InsertLineFromCsv(line);
            Console.WriteLine($"Added line {index + 1}/{size}");
        }
    }

    public async Task<IEnumerable<LineDbEntity>> GetAllLines()
    {
        await using NpgsqlConnection connection = new(_connectionString);
        return await connection.QueryAsync<LineDbEntity>("select * from line");
    }

    public async Task<IEnumerable<QuoteDbEntity>> GetAllQuotes()
    {
        await using NpgsqlConnection connection = new(_connectionString);
        return await connection.QueryAsync<QuoteDbEntity>("select * from quote");
    }

    public async Task InsertQuoteFromLine(LineDbEntity line)
    {
        await using NpgsqlConnection connection = new(_connectionString);

        var existingQuote = await connection.GetExistingQuote(line.LineText, line.Speaker);

        if (existingQuote != null)
        {
            var quoteId = existingQuote.QuoteId;
            var newScore = existingQuote.Score + 1;
            await connection.IncrementQuoteScore(quoteId, newScore);
            await connection.UpdateLineWithQuoteId(quoteId, line.LineId);
        }
        else
        {
            var quoteId = line.LineId;
            await connection.InsertNewQuote(quoteId, line.LineText, line.Speaker);
            await connection.UpdateLineWithQuoteId(quoteId, line.LineId);
        }
    }
}

[SuppressMessage("ReSharper", "RedundantAnonymousTypePropertyName")]
[SuppressMessage("ReSharper", "ArrangeTrailingCommaInMultilineLists")]
internal static class PostgreSqlRepositoryExtensions
{
    internal static async Task<QuoteDbEntity?> GetExistingQuote(this IDbConnection connection, string quote, string speaker) =>
        await connection.QueryFirstOrDefaultAsync<QuoteDbEntity>(
            "select * from quote where quote.quote like @Quote and quote.speaker like @Speaker", new
            {
                Quote = quote,
                Speaker = speaker,
            }
        );

    internal static async Task InsertNewQuote(this IDbConnection connection, int quoteId, string quote, string speaker) =>
        await connection.ExecuteAsync(
            "insert into quote (quote_id, quote, speaker, score) " +
            "values (@QuoteId, @Quote, @Speaker, @Score)", new
            {
                QuoteId = quoteId,
                Quote = quote,
                Speaker = speaker,
                Score = 1,
            }
        );

    internal static async Task UpdateLineWithQuoteId(this IDbConnection connection, int quoteId, int lineId) =>
        await connection.ExecuteAsync(
            "update line set quote_id = @QuoteId where line_id = @LineId", new
            {
                QuoteId = quoteId,
                LineId = lineId,
            }
        );

    internal static async Task IncrementQuoteScore(this IDbConnection connection, int quoteId, int newScore) =>
        await connection.ExecuteAsync(
            "update quote set score = @Score where quote_id = @QuoteId", new
            {
                Score = newScore,
                QuoteId = quoteId,
            }
        );

    internal static async Task InsertLineFromCsv(this NpgsqlConnection connection, LineFromCsv line) =>
        await connection.ExecuteAsync(
            "insert into line (line_id, season, episode, scene, line_text, speaker, deleted) " +
            "values (@LineId, @Season, @Episode, @Scene, @LineText, @Speaker, @Deleted)",
            new
            {
                LineId = line.Id,
                Season = line.Season,
                Episode = line.Episode,
                Scene = line.Scene,
                LineText = line.LineText,
                Speaker = line.Speaker,
                Deleted = line.Deleted
            }
        );
}
