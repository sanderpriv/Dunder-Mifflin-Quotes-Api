using Api.Entities;
using Api.Dtos;

namespace Api.Services;

public interface IQuotesService
{
    public Task<IEnumerable<Quote>> GetQuotes(int length);
    public Task<Quote> GetRandomQuote();
}