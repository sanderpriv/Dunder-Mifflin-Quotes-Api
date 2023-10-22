using Dunder.Mifflin.Api.DB.Entities;
using Dunder.Mifflin.Api.Repositories;

namespace Dunder.Mifflin.Api.Services.Impl;

public class LinesService : ILinesService
{
    private readonly IDbRepository _dbRepository;
    private readonly Random _random = new();

    public LinesService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<IEnumerable<LineDbEntity>> GetLines(int size)
    {
        var lines = await _dbRepository.GetAllLines();
        var result = lines.OrderBy(_ => Guid.NewGuid()).Take(size);
        return result;
    }

    public async Task<IEnumerable<LineDbEntity>> GetAllLines()
    {
        return await _dbRepository.GetAllLines();
    }

    public async Task<LineDbEntity?> GetRandomLine()
    {
        var line = (await _dbRepository.GetAllLines()).ToList();
        if (line.Count == 0)
            return null;
        
        var ran = _random.Next(0, line.Count);
        var result = line[ran];
        return result;
    }
}