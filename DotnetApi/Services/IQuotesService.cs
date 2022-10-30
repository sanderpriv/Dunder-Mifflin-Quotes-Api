using DotnetApi.Entities;

namespace DotnetApi.Services;

public interface IQuotesService
{
    public Task<IEnumerable<Quote>> GetQuotes(int length);
    public Task<IEnumerable<Quote>> GetAllQuotes();
    public Task<Quote?> GetRandomQuote();
}