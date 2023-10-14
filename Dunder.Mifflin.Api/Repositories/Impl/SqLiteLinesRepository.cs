using Dunder.Mifflin.Api.DB;
using Dunder.Mifflin.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dunder.Mifflin.Api.Repositories.Impl;

public class SqLiteLinesRepository : ILinesRepository
{
    public async Task<IEnumerable<LineDbEntity>> GetAllLines()
    {
        await using var db = new Database();
        return await db.Lines.ToListAsync();
    }

    public async Task SaveLinesFromCsvFileToDbIfDbEmpty()
    {
        await using var db = new Database();
        var length = db.Lines.Count();
        if (length != 0)
            return;

        var quotes = PersistentDataFetcher.GetLinesFromCsvFile();
        db.Lines.AddRange(quotes);
        await db.SaveChangesAsync();
    }
}