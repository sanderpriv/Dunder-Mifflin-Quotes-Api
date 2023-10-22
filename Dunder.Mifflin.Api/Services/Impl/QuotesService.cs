using Dunder.Mifflin.Api.DB.Entities;
using Dunder.Mifflin.Api.Repositories;

namespace Dunder.Mifflin.Api.Services.Impl;

public class QuotesService : IQuotesService
{
    private readonly IDbRepository _dbRepository;
    private readonly Random _random = new();

    public QuotesService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }


    public async Task<IEnumerable<QuoteDbEntity>> GetAllQuotes() => await _dbRepository.GetAllQuotes();

    public async Task<QuoteDbEntity?> GetRandomQuote()
    {
        var quotes = (await _dbRepository.GetAllQuotes()).ToList();
        if (quotes.Count == 0)
            return null;
        
        var ran = _random.Next(0, quotes.Count);
        var result = quotes[ran];
        return result;
    }
}