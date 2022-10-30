using DotnetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

[ApiController]
[Route("redditQuotes")]
public class RedditQuotesController : ControllerBase
{

    private readonly IQuotesService quotesService;
    private readonly IRedditService redditService;
    private readonly IMatchingService matchingService;

    public RedditQuotesController(IQuotesService quotesService, IRedditService redditService, IMatchingService matchingService)
    {
        this.quotesService = quotesService;
        this.redditService = redditService;
        this.matchingService = matchingService;
    }

    [HttpGet]
    [Route("")]
    public async Task<Dictionary<string, int>> GetQuotesFromLast24Hours()
    {
        var quotes = await quotesService.GetAllQuotes();
        var redditComments = await redditService.GetCommentsFromLast24Hours();
        var matches = matchingService.GetMatchesOfRedditCommentsAndQuotes(redditComments, quotes);
        return matches;
    }
}