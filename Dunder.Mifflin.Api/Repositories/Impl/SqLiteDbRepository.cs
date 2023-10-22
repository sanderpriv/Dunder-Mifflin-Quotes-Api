using Dunder.Mifflin.Api.DB;
using Dunder.Mifflin.Api.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dunder.Mifflin.Api.Repositories.Impl;

public class SqLiteDbRepository : IDbRepository
{
    public async Task SaveLinesFromCsvFileToDbIfDbEmpty()
    {
        await using var db = new Database();
        var length = db.Lines.Count();
        if (length != 0)
            return;

        var quotes = PersistentDataFetcher.GetLinesFromCsvFile();
        db.Lines.AddRange(quotes);
        await db.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<LineDbEntity>> GetAllLines()
    {
        await using var db = new Database();
        return await db.Lines.ToListAsync();
    }
    
    public async Task<IEnumerable<QuoteDbEntity>> GetAllQuotes()
    {
        await using var db = new Database();
        return await db.Quotes.ToListAsync();
    }

    public async Task InsertQuote(string quote, string speaker)
    {
        await using var db = new Database();

        var existingQuote = await db.Quotes.FirstOrDefaultAsync(q => q.Quote == quote && q.Speaker == speaker);

        if (existingQuote != null)
        {
            existingQuote.Score++;
            db.Quotes.Update(existingQuote);
            await db.SaveChangesAsync();
            return;
        }

        var quoteDbEntity = new QuoteDbEntity
        {
            Id = Guid.NewGuid(),
            Quote = quote,
            Speaker = speaker,
            Score = 1,
        };

        db.Quotes.Add(quoteDbEntity);
        await db.SaveChangesAsync();
    }
}