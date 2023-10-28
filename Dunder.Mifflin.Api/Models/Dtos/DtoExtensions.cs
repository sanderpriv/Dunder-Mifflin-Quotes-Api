using Dunder.Mifflin.Api.Models.DB;

namespace Dunder.Mifflin.Api.Models.Dtos;

public static class DtoExtensions
{
    public static LineDto AsLineDto(this LineDbEntity l)
    {
        return new LineDto(l.LineId, l.Season, l.Episode, l.Scene, l.LineText, l.Speaker, l.Deleted);
    }

    public static QuoteDto AsQuoteDto(this QuoteDbEntity q)
    {
        return new QuoteDto(q.Quote, q.Speaker, q.Score);
    }
}
