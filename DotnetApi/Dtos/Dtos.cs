namespace DotnetApi.Dtos;

public record GetQuoteDto(long Id, int Season, int Episode, int Scene, string LineText, string Speaker, bool Deleted);
public record GetQuoteWithMatchesDto(GetQuoteDto Quote, int Matches);