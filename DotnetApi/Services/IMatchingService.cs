using DotnetApi.Entities;

namespace DotnetApi.Services;

public interface IMatchingService
{
    public Dictionary<string, int> GetMatchesOfRedditCommentsAndQuotes
        (IEnumerable<string> originalComments, IEnumerable<Quote> originalQuotes);
}