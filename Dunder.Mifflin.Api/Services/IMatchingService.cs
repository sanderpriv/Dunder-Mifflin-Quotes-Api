using Dunder.Mifflin.Api.Entities;

namespace Dunder.Mifflin.Api.Services;

public interface IMatchingService
{
    public IEnumerable<QuoteWithMatches> GetMatchesOfRedditCommentsAndQuotes
        (IEnumerable<string> comments, IEnumerable<LineDbEntity> quotes);
}