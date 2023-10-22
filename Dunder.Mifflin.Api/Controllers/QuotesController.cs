using Dunder.Mifflin.Api.Models.Dtos;
using Dunder.Mifflin.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dunder.Mifflin.Api.Controllers;

[ApiController]
[Route("quotes")]
public class QuotesController : ControllerBase
{

    private readonly IQuotesService _quotesService;

    public QuotesController(IQuotesService quotesService)
    {
        _quotesService = quotesService;
    }

    [HttpGet]
    [Route("random")]
    public async Task<ActionResult<QuoteDto>> GetRandomQuote()
    {
        var quote = await _quotesService.GetRandomQuote();
        if (quote == null)
            return NoContent();
        
        return quote.AsQuoteDto();
    }

    [HttpGet]
    [Route("all")]
    public async Task<IEnumerable<QuoteDto>> GetQuotes()
    {
        return (await _quotesService.GetAllQuotes()).Select(q => q.AsQuoteDto()).OrderByDescending(q => q.Score);
    }
}