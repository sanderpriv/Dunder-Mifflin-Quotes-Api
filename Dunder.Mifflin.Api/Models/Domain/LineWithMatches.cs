using Dunder.Mifflin.Api.DB.Entities;

namespace Dunder.Mifflin.Api.Models.Domain;

public class LineWithMatches
{
    public LineDbEntity LineDbEntity;
    public int Matches;

    public LineWithMatches(LineDbEntity lineDbEntity)
    {
        LineDbEntity = lineDbEntity;
        Matches = 1;
    }
}