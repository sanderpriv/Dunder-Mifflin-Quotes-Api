using Dunder.Mifflin.Api.Models.DB;
using Dunder.Mifflin.Api.Models.Domain;

namespace Dunder.Mifflin.Api.Services;

public interface IMatchingService
{
    public Task<IEnumerable<LineWithMatches>> MatchRedditCommentsWithLines
        (IEnumerable<string> comments, IEnumerable<LineDbEntity> lines);
}