namespace Dunder.Mifflin.Api.Models.Dtos;

public record LineDto(long Id, int Season, int Episode, int Scene, string LineText, string Speaker, bool Deleted);
public record LineWithMatchesDto(LineDto Line, int Matches);

public record QuoteDto(string Quote, string Speaker, int Score);