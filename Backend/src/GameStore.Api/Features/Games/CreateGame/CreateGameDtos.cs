using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Features.Games.CreateGame;

public record CreateGameDto(
[Required]
[StringLength(50)]
string Name,
Guid GenereId,
[Range(0,100)]
decimal Price,
DateOnly ReleaseDate,
[Required]
[StringLength(500)]
string Description)
{
    public IFormFile? ImageFile { get; set; }
}
public record GameDetailsDto(Guid Id,
string Name,
Guid GenereId,
decimal Price,
DateOnly RealeaseDate,
string Description,
string ImageUri,
string LastUpdatedBy);


