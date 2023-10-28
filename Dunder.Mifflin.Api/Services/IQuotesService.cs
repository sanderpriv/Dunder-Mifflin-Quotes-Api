using Dunder.Mifflin.Api.Models.DB;

namespace Dunder.Mifflin.Api.Services;

public interface IQuotesService
{
    public Task<IEnumerable<QuoteDbEntity>> GetAllQuotes();
    public Task<QuoteDbEntity?> GetRandomQuote();
}