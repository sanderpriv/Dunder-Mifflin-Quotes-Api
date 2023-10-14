using Dunder.Mifflin.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dunder.Mifflin.Api.DB;

public class Database : DbContext
{
    public DbSet<LineDbEntity> Lines { get; set; }
    public DbSet<QuoteDbEntity> Quotes { get; set; }
    private static string DbPath => PersistentDataFetcher.k_SqLiteDbSetFilename;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}