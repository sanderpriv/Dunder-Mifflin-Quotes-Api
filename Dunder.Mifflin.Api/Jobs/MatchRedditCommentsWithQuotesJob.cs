using Dunder.Mifflin.Api.Services;
using Quartz;

namespace Dunder.Mifflin.Api.Jobs;

// ReSharper disable once ClassNeverInstantiated.Global
public class MatchRedditCommentsWithQuotesJob : IJob
{

    private readonly ILinesService _linesService;
    private readonly IRedditService _redditService;
    private readonly IMatchingService _matchingService;
    
    public MatchRedditCommentsWithQuotesJob(ILinesService linesService, IRedditService redditService, IMatchingService matchingService)
    {
        _linesService = linesService;
        _redditService = redditService;
        _matchingService = matchingService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Starting MatchRedditCommentsWithQuotes job");
        var lines = await _linesService.GetAllLines();
        var comments = await _redditService.GetCommentsFromLast24Hours();
        await _matchingService.MatchRedditCommentsWithLines(comments, lines);
        Console.WriteLine("Finished MatchRedditCommentsWithQuotes job");
    }
}