using Dunder.Mifflin.Api.DB.Entities;
using Dunder.Mifflin.Api.Models.Domain;

namespace Dunder.Mifflin.Api.Services;

public interface IMatchingService
{
    public IEnumerable<QuoteWithMatches> GetMatchesOfRedditCommentsAndQuotes
        (IEnumerable<string> comments, IEnumerable<LineDbEntity> quotes);
}