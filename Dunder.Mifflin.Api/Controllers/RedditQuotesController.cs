using Dunder.Mifflin.Api.Models.Dtos;
using Dunder.Mifflin.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dunder.Mifflin.Api.Controllers;

[ApiController]
[Route("redditQuotes")]
public class RedditQuotesController : ControllerBase
{

    private readonly IQuotesService _quotesService;
    private readonly IRedditService _redditService;
    private readonly IMatchingService _matchingService;

    public RedditQuotesController(IQuotesService quotesService, IRedditService redditService, IMatchingService matchingService)
    {
        _quotesService = quotesService;
        _redditService = redditService;
        _matchingService = matchingService;
    }

    [HttpGet]
    [Route("24h")]
    public async Task<IEnumerable<GetQuoteWithMatchesDto>> GetQuotesWithMatchesFromLast24Hours()
    {
        var quotes = await _quotesService.GetAllQuotes();
        var comments = await _redditService.GetCommentsFromLast24Hours();
        var matches = _matchingService.GetMatchesOfRedditCommentsAndQuotes(comments, quotes);
        return matches.Select(q => new GetQuoteWithMatchesDto(q.LineDbEntity.AsGetQuoteDto(), q.Matches)).OrderByDescending(q => q.Matches);
    }

    [HttpGet]
    [Route("postPermalink")]
    public async Task<IEnumerable<GetQuoteWithMatchesDto>> GetQuotesFromPostPermalink(string permalink)
    {
        var quotes = await _quotesService.GetAllQuotes();
        var comments = await _redditService.GetCommentsFromPostPermalink(permalink);
        var matches = _matchingService.GetMatchesOfRedditCommentsAndQuotes(comments, quotes);
        return matches.Select(q => new GetQuoteWithMatchesDto(q.LineDbEntity.AsGetQuoteDto(), q.Matches)).OrderByDescending(q => q.Matches);
    }
}