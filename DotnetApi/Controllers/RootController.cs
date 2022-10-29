using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

[ApiController]
[Route("/")]
public class RootController
{
    [HttpGet]
    public string Root()
    {
        return "OK";
    }
}