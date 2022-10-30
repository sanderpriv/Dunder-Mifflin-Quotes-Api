using DotnetApi.Dtos;
using DotnetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetApi.Controllers;

[ApiController]
[Route("characters")]
public class CharactersController : ControllerBase
{

    private readonly ICharactersService charactersService;

    public CharactersController(ICharactersService charactersService)
    {
        this.charactersService = charactersService;
    }

    [HttpGet]
    [Route("/{name}")]
    public async Task<ActionResult<GetCharacterDto>> GetCharacter(string name)
    {
        var character = await charactersService.GetCharacter(name);
        if (character == null)
            return NoContent();
        
        return character.AsGetCharacterDto();
    }

    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<GetCharacterDto>> GetCharacters()
    {
        return (await charactersService.GetCharacters()).AsGetCharacterDtos();
    }
}