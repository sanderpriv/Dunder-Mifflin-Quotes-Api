using DotnetApi.DB.Data;
using DotnetApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.DB.Impl;

public class SqLiteCharactersRepository : ICharactersRepository
{
    
    public async Task Create(Character character)
    {
        await using var db = new Database();
        db.Characters.Add(character);
        await db.SaveChangesAsync();
    }

    public async Task CreateMany(IEnumerable<Character> characters)
    {
        await using var db = new Database();
        db.Characters.AddRange(characters);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        await using var db = new Database();
        return await db.Characters.ToListAsync();
    }

    public async Task<Character?> Get(string name)
    {
        await using var db = new Database();
        return await db.Characters.FindAsync(name);
    }

    public async Task Update(Character character)
    {
        await using var db = new Database();
        db.Characters.Update(character);
        await db.SaveChangesAsync();
    }

    public async Task Delete(string name)
    {
        await using var db = new Database();
        var character = await Get(name);
        if (character is null)
            return;

        db.Characters.Remove(character);
        await db.SaveChangesAsync();
    }

    public async Task WarmUp()
    {
        await using var db = new Database();
        var length = db.Characters.Count();
        if (length != 0)
            return;

        var characters = PersistentDataFetcher.GetCharactersFromCsvFile();
        await CreateMany(characters);
    }
}