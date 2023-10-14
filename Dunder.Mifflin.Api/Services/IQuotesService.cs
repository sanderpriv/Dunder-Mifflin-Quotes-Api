using Dunder.Mifflin.Api.Entities;

namespace Dunder.Mifflin.Api.Services;

public interface IQuotesService
{
    public Task<IEnumerable<Quote>> GetQuotes(int length);
    public Task<IEnumerable<Quote>> GetAllQuotes();
    public Task<Quote?> GetRandomQuote();
}