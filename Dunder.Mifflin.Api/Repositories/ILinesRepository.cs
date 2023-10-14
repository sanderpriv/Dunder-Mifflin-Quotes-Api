using Dunder.Mifflin.Api.Entities;

namespace Dunder.Mifflin.Api.Repositories;

public interface ILinesRepository
{
    public Task<IEnumerable<LineDbEntity>> GetAllLines();
    public Task SaveLinesFromCsvFileToDbIfDbEmpty();
}