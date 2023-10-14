namespace Dunder.Mifflin.Api.Repositories;

public interface IRedditRepository
{
    public Task<IEnumerable<string>> GetPostPermalinksFromLast24Hours();
    public Task<IEnumerable<string>> GetTopLevelCommentsFromPostPermalink(string permalink);
}