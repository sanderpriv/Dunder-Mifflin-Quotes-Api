using DotnetApi.Dtos;
using DotnetApi.Entities;

namespace DotnetApi.Services.Impl;

public class MatchingService : IMatchingService
{
    public IEnumerable<QuoteWithMatches> GetMatchesOfRedditCommentsAndQuotes(IEnumerable<string> comments, IEnumerable<Quote> quotes)
    {
        var commentsWithMoreThanTwoWords = comments.Where(t => t.Split(" ").Length > 2);
        var filteredComments = commentsWithMoreThanTwoWords.Select(Filter).ToList();

        var quotesWithMatches = new List<QuoteWithMatches>();

        foreach (var quote in quotes)
        {
            var filteredLine = Filter(quote.LineText);
            if (!filteredComments.Contains(filteredLine)) 
                continue;
            
            if (quotesWithMatches.Select(s => s.Quote.Id).Contains(quote.Id))
            {
                quotesWithMatches.First(s => s.Quote.Id == quote.Id).Matches++;
            }
            else
            {
                quotesWithMatches.Add(new QuoteWithMatches(quote));
            }
        }

        return quotesWithMatches;
    }

    private static string Filter(string s)
    {
        return s.ToLowerInvariant()
            .Replace(" ", "")
            .Replace(",", "")
            .Replace(".", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace("'", "");
    }
}