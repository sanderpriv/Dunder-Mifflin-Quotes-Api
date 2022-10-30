using DotnetApi.Entities;

namespace DotnetApi.Services.Impl;

public class MatchingService : IMatchingService
{
    public Dictionary<string, int> GetMatchesOfRedditCommentsAndQuotes(IEnumerable<string> originalComments, IEnumerable<Quote> originalQuotes)
    {
        var comments = Filter(originalComments).ToList();
        var quotes = Filter(originalQuotes.Select(t => t.LineText));
        
        var matchesWithId = new Dictionary<string, int>();

        foreach (var quote in quotes)
        {
            if (!comments.Contains(quote)) 
                continue;
            
            if (matchesWithId.ContainsKey(quote))
            {
                matchesWithId[quote]++;
            }
            else
            {
                matchesWithId.Add(quote, 1);
            }
        }

        return matchesWithId;
    }

    private static IEnumerable<string> Filter(IEnumerable<string> strings)
    {
        var lowercased = strings.Select(s => s.ToLowerInvariant());
        var removePunctuations = RemovePunctuations(lowercased);
        return removePunctuations;
    }

    private static IEnumerable<string> RemovePunctuations(IEnumerable<string> strings)
    {
        return strings.Select(s => s
                .Replace(",", "")
                .Replace(".", "")
                .Replace("!", "")
                .Replace("?", "")
                .Replace("'", ""))
            ;
    }
}