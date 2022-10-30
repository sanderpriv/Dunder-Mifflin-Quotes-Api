using DotnetApi.Entities;

namespace DotnetApi.Services;

public interface ICharactersService
{
    public Task<IEnumerable<Character>> GetCharacters();
    public Task<Character?> GetCharacter(string name);
}