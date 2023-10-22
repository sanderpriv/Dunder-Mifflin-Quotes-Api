using Dunder.Mifflin.Api.Models.Dtos;
using Dunder.Mifflin.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dunder.Mifflin.Api.Controllers;

[ApiController]
public class QuotesController : ControllerBase
{

    private readonly IQuotesService _quotesService;

    public QuotesController(IQuotesService quotesService)
    {
        _quotesService = quotesService;
    }

    [HttpGet]
    [Route("quote")]
    public async Task<ActionResult<GetQuoteDto>> GetRandomQuote()
    {
        var quote = await _quotesService.GetRandomQuote();
        if (quote == null)
            return NoContent();
        
        return quote.AsGetQuoteDto();
    }

    [HttpGet]
    [Route("quotes")]
    public async Task<IEnumerable<GetQuoteDto>> GetQuotes(int size = 10)
    {
        return (await _quotesService.GetQuotes(size)).Select(q => q.AsGetQuoteDto());
    }
}