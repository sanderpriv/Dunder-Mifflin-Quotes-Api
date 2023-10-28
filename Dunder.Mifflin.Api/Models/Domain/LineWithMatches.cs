using Dunder.Mifflin.Api.Models.DB;

namespace Dunder.Mifflin.Api.Models.Domain;

public class LineWithMatches
{
    public LineDbEntity LineDbEntity { get; }
    public int Matches { get; set; }

    public LineWithMatches(LineDbEntity lineDbEntity)
    {
        LineDbEntity = lineDbEntity;
        Matches = 1;
    }
}