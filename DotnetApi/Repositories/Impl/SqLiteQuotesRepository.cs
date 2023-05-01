using DotnetApi.DB;
using DotnetApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.Repositories.Impl;

public class SqLiteQuotesRepository : IQuotesRepository
{
    
    public async Task Create(Quote quote)
    {
        await using var db = new Database();
        db.Quotes.Add(quote);
        await db.SaveChangesAsync();
    }

    public async Task CreateMany(IEnumerable<Quote> quotes)
    {
        await using var db = new Database();
        db.Quotes.AddRange(quotes);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Quote>> GetAll()
    {
        await using var db = new Database();
        return await db.Quotes.ToListAsync();
    }

    public async Task<Quote?> Get(long id)
    {
        await using var db = new Database();
        return await db.Quotes.FindAsync(id);
    }

    public async Task Update(Quote quote)
    {
        await using var db = new Database();
        db.Quotes.Update(quote);
        await db.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        await using var db = new Database();
        var quote = await Get(id);
        if (quote is null)
            return;

        db.Quotes.Remove(quote);
        await db.SaveChangesAsync();
    }

    public async Task WarmUp()
    {
        await using var db = new Database();
        var length = db.Quotes.Count();
        if (length != 0)
            return;

        var quotes = PersistentDataFetcher.GetQuotesFromCsvFile();
        await CreateMany(quotes);
    }
}