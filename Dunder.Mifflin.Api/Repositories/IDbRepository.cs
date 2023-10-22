using Dunder.Mifflin.Api.DB.Entities;

namespace Dunder.Mifflin.Api.Repositories;

public interface IDbRepository
{
    public Task SaveLinesFromCsvFileToDbIfDbEmpty();
    public Task<IEnumerable<LineDbEntity>> GetAllLines();
    public Task<IEnumerable<QuoteDbEntity>> GetAllQuotes();
    public Task InsertQuote(string quote, string speaker);
}