using DotnetApi.DB;
using DotnetApi.Entities;

namespace DotnetApi.Services.Impl;

public class CharactersService : ICharactersService
{
    private readonly ICharactersRepository charactersRepository;

    public CharactersService(ICharactersRepository charactersRepository)
    {
        this.charactersRepository = charactersRepository;
    }

    public async Task<IEnumerable<Character>> GetCharacters()
    {
        return await charactersRepository.GetAll();
    }

    public async Task<Character?> GetCharacter(string name)
    {
        return await charactersRepository.Get(name);
    }
}