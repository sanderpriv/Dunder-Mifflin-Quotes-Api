using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("quotes")]
public class QuotesController : ControllerBase
{

    private readonly IQuotesService quotesService;

    public QuotesController(IQuotesService quotesService)
    {
        this.quotesService = quotesService;
    }

    [HttpGet]
    [Route("random")]
    public async Task<ActionResult<GetQuoteDto>> GetRandomQuotes()
    {
        var quote = await quotesService.GetRandomQuote();
        if (quote == null)
            return NoContent();
        
        return quote.AsGetQuoteDto();
    }

    [HttpGet]
    [Route("{size:int}")]
    public async Task<IEnumerable<GetQuoteDto>> GetQuotes(int size)
    {
        return (await quotesService.GetQuotes(size)).AsGetQuoteDtos();
    }
}