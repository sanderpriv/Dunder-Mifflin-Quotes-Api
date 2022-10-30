using DotnetApi.DB;
using DotnetApi.Entities;

namespace DotnetApi.Services.Impl;

public class QuotesService : IQuotesService
{
    private readonly IQuotesRepository quotesRepository;
    private readonly Random random = new();

    public QuotesService(IQuotesRepository quotesRepository)
    {
        this.quotesRepository = quotesRepository;
    }

    public async Task<IEnumerable<Quote>> GetQuotes(int size)
    {
        var quotes = await quotesRepository.GetAll();
        var result = quotes.OrderBy(_ => Guid.NewGuid()).Take(size);
        return result;
    }

    public async Task<IEnumerable<Quote>> GetAllQuotes()
    {
        return await quotesRepository.GetAll();
    }

    public async Task<Quote?> GetRandomQuote()
    {
        var quotes = (await quotesRepository.GetAll()).ToList();
        if (quotes.Count == 0)
            return null;
        
        var ran = random.Next(0, quotes.Count);
        var result = quotes[ran];
        return result;
    }
}