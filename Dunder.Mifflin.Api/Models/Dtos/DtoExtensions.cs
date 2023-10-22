using Dunder.Mifflin.Api.DB.Entities;

namespace Dunder.Mifflin.Api.Models.Dtos;

public static class DtoExtensions
{
    public static GetQuoteDto AsGetQuoteDto(this LineDbEntity q)
    {
        return new GetQuoteDto(q.Id, q.Season, q.Episode, q.Scene, q.LineText, q.Speaker, q.Deleted);
    }
}
