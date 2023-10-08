using DotnetApi.Dtos;
using DotnetApi.Entities;

namespace DotnetApi.Services.Impl;

public class MatchingService : IMatchingService
{
    private const int DamerauLevenshteinDistanceThreshold = 5;
    
    public IEnumerable<QuoteWithMatches> GetMatchesOfRedditCommentsAndQuotes(IEnumerable<string> comments, IEnumerable<Quote> quotes)
    {
        var commentsWithMoreThanTwoWords = comments.Where(t => t.Split(" ").Length > 2).ToList();

        var quotesWithMatches = new List<QuoteWithMatches>();

        foreach (var quote in quotes)
        {
            var line = quote.LineText;

            foreach (var comment in commentsWithMoreThanTwoWords)
            {
                var damerauLevenshteinDistance = CalculateDamerauLevenshteinDistance(line, comment);
                if (damerauLevenshteinDistance > DamerauLevenshteinDistanceThreshold)
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

    // https://gist.github.com/wickedshimmy/449595/a17ab0d689623f5e6730eeb1c8606ab771149819
    public static int CalculateDamerauLevenshteinDistance(string first, string second)
    {
        if (first == second) {
            return 0;
        }

        var lengthFirst = first.Length;
        var lengthSecond = second.Length;
        if (lengthFirst == 0 || lengthSecond == 0)
        {
            return lengthFirst == 0 ? lengthSecond : lengthFirst;
        }

        var matrix = new int[lengthFirst + 1, lengthSecond + 1];

        for (var i = 1; i <= lengthFirst; i++) {
            matrix[i, 0] = i;
            for (var j = 1; j <= lengthSecond; j++) {
                var cost = second[j - 1] == first[i - 1] ? 0 : 1;
                if (i == 1)
                    matrix[0, j] = j;

                var vals = new[] {
                    matrix[i - 1, j] + 1,
                    matrix[i, j - 1] + 1,
                    matrix[i - 1, j - 1] + cost
                };
                matrix[i,j] = vals.Min();
                if (i > 1 && j > 1 && first[i - 1] == second[j - 2] && first[i - 2] == second[j - 1])
                    matrix[i,j] = Math.Min (matrix[i,j], matrix[i - 2, j - 2] + cost);
            }
        }
        return matrix[lengthFirst, lengthSecond];
    }
}