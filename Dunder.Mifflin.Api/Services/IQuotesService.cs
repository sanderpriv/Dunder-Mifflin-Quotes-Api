using Dunder.Mifflin.Api.DB.Entities;

namespace Dunder.Mifflin.Api.Services;

public interface IQuotesService
{
    public Task<IEnumerable<LineDbEntity>> GetQuotes(int length);
    public Task<IEnumerable<LineDbEntity>> GetAllQuotes();
    public Task<LineDbEntity?> GetRandomQuote();
}