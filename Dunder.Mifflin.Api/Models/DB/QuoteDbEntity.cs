namespace Dunder.Mifflin.Api.Models.DB;

public class QuoteDbEntity
{
    public int QuoteId { get; set; }
    public string Quote { get; set; }
    public string Speaker { get; set; }
    public int Score { get; set; }
}