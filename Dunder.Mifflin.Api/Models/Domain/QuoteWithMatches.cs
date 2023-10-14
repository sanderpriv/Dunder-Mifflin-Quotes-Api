namespace Dunder.Mifflin.Api.Entities;

public class QuoteWithMatches
{
    public LineDbEntity LineDbEntity;
    public int Matches;

    public QuoteWithMatches(LineDbEntity lineDbEntity)
    {
        LineDbEntity = lineDbEntity;
        Matches = 1;
    }
}