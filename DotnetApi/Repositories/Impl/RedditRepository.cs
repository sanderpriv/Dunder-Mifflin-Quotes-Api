using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotnetApi.Repositories.Impl;

public class RedditRepository : IRedditRepository
{
    private const string UserAgent = "Dunder Mifflin Quotes API";
    private const int PostsLimit = 10;
    private const int CommentsLimit = 10;
    
    public async Task<IEnumerable<string>> GetPostPermalinksFromLast24Hours()
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        
        var stream = await client.GetStreamAsync($"https://www.reddit.com/r/DunderMifflin/top.json?limit={PostsLimit}");

        var listing = await JsonSerializer.DeserializeAsync<Listing>(stream) ?? throw new Exception("Could not fetch new posts from r/DunderMifflin");
        var permalinks = listing.Data.Children.Where(c => c != null).Select(c => c!.Data.Permalink);
        return permalinks;
    }

    public async Task<IEnumerable<string>> GetTopLevelCommentsFromPostPermalink(string permalink)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

        var url = $"https://www.reddit.com{permalink}.json?sort=top&limit={CommentsLimit}";
        var stream = await client.GetStreamAsync(url);
        try
        {
            var listings = await JsonSerializer.DeserializeAsync<IEnumerable<Listing>>(stream) ?? throw new Exception($"Could not fetch comments from {permalink}");
            
            var comments =
                from listing in listings
                from child in listing.Data.Children
                where child.Kind == "t1"
                select child.Data.Body;

            return comments;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<string>();
        }
    }
}

public record Listing(
    [property: JsonPropertyName("data")] ListingData Data
);

public record ListingData(
    [property: JsonPropertyName("children")] IEnumerable<Child?> Children,
    [property: JsonPropertyName("permalink")] string Permalink,
    [property: JsonPropertyName("body")] string Body
);
public record Child(
    [property: JsonPropertyName("kind")] string Kind,
    [property: JsonPropertyName("data")] ListingData Data
);