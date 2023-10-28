using Dunder.Mifflin.Api.Models.DB;

namespace Dunder.Mifflin.Api.Services;

public interface ILinesService
{
    public Task<IEnumerable<LineDbEntity>> GetLines(int length);
    public Task<IEnumerable<LineDbEntity>> GetAllLines();
}