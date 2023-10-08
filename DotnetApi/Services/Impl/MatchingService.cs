using DotnetApi.Entities;
using Quickenshtein;

namespace DotnetApi.Services.Impl;

public class MatchingService : IMatchingService
{
    private const int DamerauLevenshteinDistanceThreshold = 5;
    
    public IEnumerable<QuoteWithMatches> GetMatchesOfRedditCommentsAndQuotes(IEnumerable<string> comments, IEnumerable<Quote> quotes)
    {
        var commentsWithMoreThanTwoWords = comments.SelectMany(i => i.Split("\n").Where(j => j.Split(" ").Length > 2)).ToList(); 
        var quotesWithMoreThanTwoWords = quotes.Where(q => q.LineText.Split(" ").Length > 2).ToList(); 
        
        var quotesWithMatches = new List<QuoteWithMatches>();

        foreach (var quote in quotesWithMoreThanTwoWords)
        {
            var line = quote.LineText;

            foreach (var comment in commentsWithMoreThanTwoWords)
            {
                var distance = Levenshtein.GetDistance(line, comment, CalculationOptions.DefaultWithThreading);
                if (distance > DamerauLevenshteinDistanceThreshold)
                {
                    continue;
                }

                if (quotesWithMatches.Select(s => s.Quote.Id).Contains(quote.Id))
                {
                    quotesWithMatches.First(s => s.Quote.Id == quote.Id).Matches++;
                }
                else
                {
                    quotesWithMatches.Add(new QuoteWithMatches(quote));
                }
            }
        }

        return quotesWithMatches;
    }
}
