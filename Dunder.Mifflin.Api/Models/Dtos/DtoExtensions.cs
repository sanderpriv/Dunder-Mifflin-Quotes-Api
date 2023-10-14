using Dunder.Mifflin.Api.Entities;

namespace Dunder.Mifflin.Api.Dtos;

public static class DtoExtensions
{
    public static GetQuoteDto AsGetQuoteDto(this LineDbEntity q)
    {
        return new GetQuoteDto(q.Id, q.Season, q.Episode, q.Scene, q.LineText, q.Speaker, q.Deleted);
    }

    public static IEnumerable<GetQuoteDto> AsGetQuoteDtos(this IEnumerable<LineDbEntity> quotes)
    {
        return quotes.Select(q => q.AsGetQuoteDto());
    }
}