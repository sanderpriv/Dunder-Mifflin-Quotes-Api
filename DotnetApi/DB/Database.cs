using DotnetApi.DB.Data;
using DotnetApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.DB;

public class Database : DbContext
{
    public DbSet<Quote> Quotes { get; set; }
    public string DbPath { get; }

    public Database()
    {
        DbPath = PersistentDataFetcher.k_SqLiteDbSetFilename;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}