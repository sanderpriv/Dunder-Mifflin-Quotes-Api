using Dunder.Mifflin.Api.Models.DB;

namespace Dunder.Mifflin.Api.Repositories;

public interface IDbRepository
{
    public Task SaveLinesFromCsvFileToDbIfDbEmpty();
    public Task<IEnumerable<LineDbEntity>> GetAllLines();
    public Task<IEnumerable<QuoteDbEntity>> GetAllQuotes();
    public Task InsertQuoteFromLine(LineDbEntity line);
}