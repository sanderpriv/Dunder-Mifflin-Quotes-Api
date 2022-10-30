namespace DotnetApi.Services;

public interface IRedditService
{
    public Task<IEnumerable<string>> GetCommentsFromLast24Hours();
}