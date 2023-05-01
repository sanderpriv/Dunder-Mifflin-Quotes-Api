using DotnetApi.Dtos;
using DotnetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

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
        return matches.Select(t => new GetQuoteWithMatchesDto(t.Quote.AsGetQuoteDto(), t.Matches));
    }

    [HttpGet]
    [Route("postPermalink")]
    public async Task<IEnumerable<GetQuoteWithMatchesDto>> GetQuotesFromPostPermalink(string permalink)
    {
        var quotes = await _quotesService.GetAllQuotes();
        var comments = await _redditService.GetCommentsFromPostPermalink("/r/DunderMifflin/comments/12d06js/favourite_oscar_line");
        var matches = _matchingService.GetMatchesOfRedditCommentsAndQuotes(comments, quotes);
        return matches.Select(t => new GetQuoteWithMatchesDto(t.Quote.AsGetQuoteDto(), t.Matches));
    }
}