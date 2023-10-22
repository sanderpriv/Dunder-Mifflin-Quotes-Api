using Dunder.Mifflin.Api.DB.Entities;
using Dunder.Mifflin.Api.Models.Domain;
using Dunder.Mifflin.Api.Repositories;
using Quickenshtein;

namespace Dunder.Mifflin.Api.Services.Impl;

public class MatchingService : IMatchingService
{
    private readonly IDbRepository _dbRepository;

    public MatchingService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<IEnumerable<LineWithMatches>> MatchRedditCommentsWithLines(IEnumerable<string> comments, IEnumerable<LineDbEntity> quotes)
    {
        var commentsWithMoreThanTwoWords = comments.SelectMany(i => i.Split("\n").Where(j => j.Split(" ").Length > 2)).ToList(); 
        var linesWithMoreThanTwoWords = quotes.Where(q => q.LineText.Split(" ").Length > 2).ToList(); 
        
        var linesWithMatches = new List<LineWithMatches>();

        foreach (var quote in linesWithMoreThanTwoWords)
        {
            var line = quote.LineText;

            foreach (var comment in commentsWithMoreThanTwoWords)
            {
                var wordCount = comment.Split(" ").Length;
                var distance = Levenshtein.GetDistance(line, comment, CalculationOptions.DefaultWithThreading);
                if (distance > wordCount)
                {
                    continue;
                }

                await _dbRepository.InsertQuote(quote.LineText, quote.Speaker);
                
                if (linesWithMatches.Select(s => s.LineDbEntity.Id).Contains(quote.Id))
                {
                    linesWithMatches.First(s => s.LineDbEntity.Id == quote.Id).Matches++;
                }
                else
                {
                    linesWithMatches.Add(new LineWithMatches(quote));
                }
            }
        }

        return linesWithMatches;
    }
}
