using Api.DB.Data;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DB;

public class QuotesContext : DbContext
{
    public DbSet<Quote> Quotes { get; set; }
    public string DbPath { get; }

    public QuotesContext()
    {
        DbPath = PersistentDataFetcher.k_SqLiteDbSetFilename;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}