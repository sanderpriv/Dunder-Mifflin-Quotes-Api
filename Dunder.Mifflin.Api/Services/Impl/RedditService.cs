using Dunder.Mifflin.Api.Repositories;

namespace Dunder.Mifflin.Api.Services.Impl;

public class RedditService : IRedditService
{
    private readonly IRedditRepository _redditRepository;

    public RedditService(IRedditRepository redditRepository)
    {
        _redditRepository = redditRepository;
    }

    public async Task<IEnumerable<string>> GetCommentsFromLast24Hours()
    {
        var permalinks = await _redditRepository.GetPostPermalinksFromLast24Hours();
        var comments = new List<string>();
        foreach (var permalink in permalinks)
        {
            comments.AddRange(await _redditRepository.GetTopLevelCommentsFromPostPermalink(permalink));
        }

        return comments;
    }

    public async Task<IEnumerable<string>> GetCommentsFromPostPermalink(string permalink)
    {
        return await _redditRepository.GetTopLevelCommentsFromPostPermalink(permalink);
    }
}