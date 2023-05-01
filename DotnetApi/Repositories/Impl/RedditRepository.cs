using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotnetApi.Repositories.Impl;

public class RedditRepository : IRedditRepository
{
    public async Task<IEnumerable<string>> GetPostPermalinksFromLast24Hours()
    {
        using HttpClient client = new();

        var stream = await client.GetStreamAsync("https://www.reddit.com/r/dundermifflin/top/.json?limit=100");
        var listing = await JsonSerializer.DeserializeAsync<Listing>(stream);
        if (listing == null)
        {
            throw new InvalidOperationException("Could not fetch posts from the last 24 hours");
        }

        var permalinks = listing.Data.Children?.Select(t => t.Data.Permalink);
        return permalinks ?? new List<string>();
    }

    public async Task<IEnumerable<string>> GetTopLevelCommentsFromPostPermalink(string permalink)
    {
        using HttpClient client = new();

        var url = $"https://www.reddit.com{permalink}.json?sort=top";
        var stream = await client.GetStreamAsync(url);
        try
        {
            var listings = await JsonSerializer.DeserializeAsync<IEnumerable<Listing>>(stream) ??
                           throw new InvalidOperationException($"Could not fetch comments from {permalink}");



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