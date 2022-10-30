namespace DotnetApi.Services.Impl;

public class RedditService : IRedditService
{
    public async Task<IEnumerable<string>> GetCommentsFromLast24Hours()
    {
        return new List<string>()
        {
            "Hey",
            "Why do you always do that? Whenever I'm getting married, you don't believe me.",
        };
    }
}