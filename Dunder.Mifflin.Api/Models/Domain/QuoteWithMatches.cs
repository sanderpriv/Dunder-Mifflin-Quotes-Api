using Dunder.Mifflin.Api.DB.Entities;

namespace Dunder.Mifflin.Api.Models.Domain;

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