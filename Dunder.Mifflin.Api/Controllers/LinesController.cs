using Dunder.Mifflin.Api.Models.Dtos;
using Dunder.Mifflin.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dunder.Mifflin.Api.Controllers;

[ApiController]
[Route("lines")]
public class LinesController : ControllerBase
{

    private readonly ILinesService _linesService;

    public LinesController(ILinesService linesService)
    {
        _linesService = linesService;
    }

    [HttpGet]
    public async Task<IEnumerable<LineDto>> GetLines(int size = 10)
    {
        return (await _linesService.GetLines(size)).Select(l => l.AsLineDto());
    }
}