using DotnetApi.Entities;

namespace DotnetApi.Services;

public interface IMatchingService
{
    public IEnumerable<QuoteWithMatches> GetMatchesOfRedditCommentsAndQuotes
        (IEnumerable<string> comments, IEnumerable<Quote> quotes);
}