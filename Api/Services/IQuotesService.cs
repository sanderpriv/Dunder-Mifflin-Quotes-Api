using Api.Entities;

namespace Api.Services;

public interface IQuotesService
{
    public Task<IEnumerable<Quote>> GetQuotes(int length);
    public Task<Quote?> GetRandomQuote();
}