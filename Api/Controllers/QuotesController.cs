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
    public async Task<GetQuoteDto> GetRandomQuotes()
    {
        return (await quotesService.GetRandomQuote()).AsGetQuoteDto();
    }

    [HttpGet]
    [Route("{size:int}")]
    public async Task<IEnumerable<GetQuoteDto>> GetQuotes(int size)
    {
        return (await quotesService.GetQuotes(size)).AsGetQuoteDtos();
    }
}