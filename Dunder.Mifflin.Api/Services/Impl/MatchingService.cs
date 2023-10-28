using Dunder.Mifflin.Api.Models.DB;
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

    public async Task<IEnumerable<LineWithMatches>> MatchRedditCommentsWithLines(IEnumerable<string> comments, IEnumerable<LineDbEntity> lines)
    {
        var commentsWithMoreThanTwoWords = comments.SelectMany(i => i.Split("\n").Where(j => j.Split(" ").Length > 2)).ToList(); 
        var linesWithMoreThanTwoWords = lines.Where(q => q.LineText.Split(" ").Length > 2).ToList(); 
        
        var linesWithMatches = new List<LineWithMatches>();

        foreach (var lineDbEntity in linesWithMoreThanTwoWords)
        {
            var line = lineDbEntity.LineText;

            foreach (var comment in commentsWithMoreThanTwoWords)
            {
                var wordCount = comment.Split(" ").Length;
                var distance = Levenshtein.GetDistance(line, comment, CalculationOptions.DefaultWithThreading);
                if (distance > wordCount)
                {
                    continue;
                }

                await _dbRepository.InsertQuoteFromLine(lineDbEntity);
                
                if (linesWithMatches.Select(s => s.LineDbEntity.LineId).Contains(lineDbEntity.LineId))
                {
                    linesWithMatches.First(s => s.LineDbEntity.LineId == lineDbEntity.LineId).Matches++;
                }
                else
                {
                    linesWithMatches.Add(new LineWithMatches(lineDbEntity));
                }
            }
        }

        return linesWithMatches;
    }
}
