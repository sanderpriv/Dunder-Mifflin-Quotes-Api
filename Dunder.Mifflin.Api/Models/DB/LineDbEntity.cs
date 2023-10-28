namespace Dunder.Mifflin.Api.Models.DB;

public class LineDbEntity
{
    public int LineId { get; set; }
    public int Season { get; set; }
    public int Episode { get; set; }
    public int Scene { get; set; }
    public string LineText { get; set; }
    public string Speaker { get; set; }
    public bool Deleted { get; set; }
    public int? QuoteId { get; set; } 
}