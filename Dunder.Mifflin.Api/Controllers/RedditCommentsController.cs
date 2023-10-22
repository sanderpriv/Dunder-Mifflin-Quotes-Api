using Dunder.Mifflin.Api.Models.Dtos;
using Dunder.Mifflin.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dunder.Mifflin.Api.Controllers;

[ApiController]
[Route("redditComments")]
public class RedditCommentsController : ControllerBase
{

    private readonly ILinesService _linesService;
    private readonly IRedditService _redditService;
    private readonly IMatchingService _matchingService;

    public RedditCommentsController(ILinesService linesService, IRedditService redditService, IMatchingService matchingService)
    {
        _linesService = linesService;
        _redditService = redditService;
        _matchingService = matchingService;
    }

    [HttpGet]
    [Route("24h")]
    public async Task<IEnumerable<LineWithMatchesDto>> GetQuotesWithMatchesFromLast24Hours()
    {
        var lines = await _linesService.GetAllLines();
        var comments = await _redditService.GetCommentsFromLast24Hours();
        var matches = await _matchingService.MatchRedditCommentsWithLines(comments, lines);
        return matches.Select(q => new LineWithMatchesDto(q.LineDbEntity.AsLineDto(), q.Matches)).OrderByDescending(q => q.Matches);
    }

    [HttpGet]
    [Route("postPermalink")]
    public async Task<IEnumerable<LineWithMatchesDto>> GetQuotesFromPostPermalink(string permalink)
    {
        var lines = await _linesService.GetAllLines();
        var comments = await _redditService.GetCommentsFromPostPermalink(permalink);
        var matches = await _matchingService.MatchRedditCommentsWithLines(comments, lines);
        return matches.Select(q => new LineWithMatchesDto(q.LineDbEntity.AsLineDto(), q.Matches)).OrderByDescending(q => q.Matches);
    }
}