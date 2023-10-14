using Dunder.Mifflin.Api.Entities;

namespace Dunder.Mifflin.Api.Repositories;

public interface IQuotesRepository
{
    public Task<IEnumerable<QuoteDbEntity>> GetAllQuotes();
    public Task InsertQuote(string quote, string speaker, LineDbEntity line);
}