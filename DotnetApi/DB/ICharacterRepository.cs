using DotnetApi.Entities;

namespace DotnetApi.DB;

public interface ICharactersRepository
{
    public Task Create(Character character);
    public Task CreateMany(IEnumerable<Character> characters);
    public Task<IEnumerable<Character>> GetAll();
    public Task<Character?> Get(string name);
    public Task Update(Character character);
    public Task Delete(string name);
    public Task WarmUp();
}