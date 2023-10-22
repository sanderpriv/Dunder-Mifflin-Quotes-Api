namespace Dunder.Mifflin.Api.DB.Entities;

public class QuoteDbEntity
{
    public Guid Id { get; set; }
    public string Quote { get; set; }
    public string Speaker { get; set; }
    public int Score { get; set; }
}