using DotnetApi.Dtos;
using DotnetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

[ApiController]
public class QuotesController : ControllerBase
{

    private readonly IQuotesService quotesService;

    public QuotesController(IQuotesService quotesService)
    {
        this.quotesService = quotesService;
    }

    [HttpGet]
    [Route("quote")]
    public async Task<ActionResult<GetQuoteDto>> GetRandomQuote()
    {
        var quote = await quotesService.GetRandomQuote();
        if (quote == null)
            return NoContent();
        
        return quote.AsGetQuoteDto();
    }

    [HttpGet]
    [Route("quotes")]
    public async Task<IEnumerable<GetQuoteDto>> GetQuotes(int size = 10)
    {
        return (await quotesService.GetQuotes(size)).AsGetQuoteDtos();
    }
}