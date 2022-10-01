using Api.Entities;

namespace Api.Services.Impl;

public class QuotesService : IQuotesService
{
    private readonly List<Quote> quotes;
    private readonly Random random = new();

    public QuotesService(List<Quote> quotes)
    {
        this.quotes = quotes;
    }

    public Task<IEnumerable<Quote>> GetQuotes(int size)
    {
        var result = quotes.OrderBy(x => Guid.NewGuid()).Take(size);
        return Task.FromResult(result);
    }

    public Task<Quote> GetRandomQuote()
    {
        var ran = random.Next(0, quotes.Count);
        var result = quotes[ran];
        return Task.FromResult(result);
    }
}