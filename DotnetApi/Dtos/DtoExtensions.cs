using DotnetApi.Entities;

namespace DotnetApi.Dtos;

public static class DtoExtensions
{
    public static GetQuoteDto AsGetQuoteDto(this Quote q)
    {
        return new GetQuoteDto(q.Id, q.Season, q.Episode, q.Scene, q.LineText, q.Speaker, q.Deleted);
    }

    public static IEnumerable<GetQuoteDto> AsGetQuoteDtos(this IEnumerable<Quote> quotes)
    {
        return quotes.Select(q => q.AsGetQuoteDto());
    }
}