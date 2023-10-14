namespace Dunder.Mifflin.Api.Entities;

public class LineDbEntity
{
    public long Id { get; set; }
    public int Season { get; set; }
    public int Episode { get; set; }
    public int Scene { get; set; }
    public string? LineText { get; set; }
    public string? Speaker { get; set; }
    public bool Deleted { get; set; }
}