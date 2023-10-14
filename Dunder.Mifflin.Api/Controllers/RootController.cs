using Microsoft.AspNetCore.Mvc;

namespace Dunder.Mifflin.Api.Controllers;

[ApiController]
[Route("/")]
public class RootController
{
    [HttpGet]
    public IActionResult Root()
    {
        return new RedirectResult("/swagger");
    }
}