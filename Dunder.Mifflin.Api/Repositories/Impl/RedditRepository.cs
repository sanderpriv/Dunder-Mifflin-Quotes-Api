using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dunder.Mifflin.Api.Repositories.Impl;

public class RedditRepository : IRedditRepository
{
    private const string UserAgent = "Dunder Mifflin Quotes API";
    
    public async Task<IEnumerable<string>> GetPostPermalinksFromLast24Hours()
    {
        try
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        
            var stream = await client.GetStreamAsync("https://www.reddit.com/r/DunderMifflin/top.json");

            var listing = await JsonSerializer.DeserializeAsync<Listing>(stream) ?? throw new Exception("Could not fetch new posts from r/DunderMifflin");
            var permalinks = listing.Data.Children.Where(c => c?.Data?.Permalink != null).Select(c => c!.Data?.Permalink);
            return permalinks!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<string>();
        }
    }

    public async Task<IEnumerable<string>> GetTopLevelCommentsFromPostPermalink(string permalink)
    {
        try
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            var url = $"https://www.reddit.com{permalink}.json?sort=top";
            var stream = await client.GetStreamAsync(url);
            
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
    [property: JsonPropertyName("children")] IEnumerable<Child?> Children
);

public record Child(
    [property: JsonPropertyName("kind")] string? Kind,
    [property: JsonPropertyName("data")] ChildData? Data
);

public record ChildData(
    [property: JsonPropertyName("permalink")] string? Permalink,
    [property: JsonPropertyName("body")] string? Body
);
