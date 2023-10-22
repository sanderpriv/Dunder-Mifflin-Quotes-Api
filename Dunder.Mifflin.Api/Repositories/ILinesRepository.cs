using Dunder.Mifflin.Api.DB.Entities;

namespace Dunder.Mifflin.Api.Repositories;

public interface ILinesRepository
{
    public Task<IEnumerable<LineDbEntity>> GetAllLines();
    public Task SaveLinesFromCsvFileToDbIfDbEmpty();
}