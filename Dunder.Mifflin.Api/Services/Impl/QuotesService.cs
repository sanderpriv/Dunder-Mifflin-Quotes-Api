using Dunder.Mifflin.Api.Entities;
using Dunder.Mifflin.Api.Repositories;

namespace Dunder.Mifflin.Api.Services.Impl;

public class QuotesService : IQuotesService
{
    private readonly ILinesRepository _linesRepository;
    private readonly Random random = new();

    public QuotesService(ILinesRepository linesRepository)
    {
        this._linesRepository = linesRepository;
    }

    public async Task<IEnumerable<LineDbEntity>> GetQuotes(int size)
    {
        var quotes = await _linesRepository.GetAllLines();
        var result = quotes.OrderBy(_ => Guid.NewGuid()).Take(size);
        return result;
    }

    public async Task<IEnumerable<LineDbEntity>> GetAllQuotes()
    {
        return await _linesRepository.GetAllLines();
    }

    public async Task<LineDbEntity?> GetRandomQuote()
    {
        var quotes = (await _linesRepository.GetAllLines()).ToList();
        if (quotes.Count == 0)
            return null;
        
        var ran = random.Next(0, quotes.Count);
        var result = quotes[ran];
        return result;
    }
}