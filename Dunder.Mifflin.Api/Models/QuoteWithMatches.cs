namespace Dunder.Mifflin.Api.Entities;

public class QuoteWithMatches
{
    public Quote Quote;
    public int Matches;

    public QuoteWithMatches(Quote quote)
    {
        Quote = quote;
        Matches = 1;
    }
}