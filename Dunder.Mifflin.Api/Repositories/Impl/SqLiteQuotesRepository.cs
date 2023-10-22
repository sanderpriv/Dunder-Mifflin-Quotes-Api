using Dunder.Mifflin.Api.DB;
using Dunder.Mifflin.Api.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dunder.Mifflin.Api.Repositories.Impl;

public class SqLiteQuotesRepository : IQuotesRepository
{
    public async Task<IEnumerable<QuoteDbEntity>> GetAllQuotes()
    {
        await using var db = new Database();
        return await db.Quotes.ToListAsync();
    }

    public async Task InsertQuote(string quote, string speaker, LineDbEntity line)
    {
        await using var db = new Database();

        var existingQuote = await db.Quotes.FirstOrDefaultAsync(q => q.Quote == quote && q.Speaker == speaker);

        if (existingQuote != null)
        {
            existingQuote.Score++;
            db.Quotes.Update(existingQuote);
            await db.SaveChangesAsync();
        }
        else
        {
            var quoteDbEntity = new QuoteDbEntity
            {
                Id = Guid.NewGuid(),
                Quote = quote,
                Speaker = speaker,
                Score = 1,
            };

            db.Quotes.Add(quoteDbEntity);
            await db.SaveChangesAsync();
        }
    }
}