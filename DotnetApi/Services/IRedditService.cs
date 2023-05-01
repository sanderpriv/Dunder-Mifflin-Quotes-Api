namespace DotnetApi.Services;

public interface IRedditService
{
    public Task<IEnumerable<string>> GetCommentsFromLast24Hours();
    public Task<IEnumerable<string>> GetCommentsFromPostPermalink(string permalink);
}