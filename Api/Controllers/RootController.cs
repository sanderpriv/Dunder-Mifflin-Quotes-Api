using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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