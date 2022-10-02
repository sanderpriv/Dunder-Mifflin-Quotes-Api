using Api.Entities;

namespace Api.DB;

public interface IQuotesRepository
{
    public Task Create(Quote quote);
    public Task CreateMany(IEnumerable<Quote> quotes);
    public Task<IEnumerable<Quote>> GetAll();
    public Task<Quote?> Get(long id);
    public Task Update(Quote quote);
    public Task Delete(long id);
    public Task WarmUp();
}