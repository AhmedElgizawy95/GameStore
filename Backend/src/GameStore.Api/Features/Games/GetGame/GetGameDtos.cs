namespace GameStore.Api.Features.Games.GetGame;



public record GameDetailsDto(Guid Id,
string Name,
Guid GenereId,
decimal Price,
DateOnly RealeaseDate,
string Description,
string ImageUri);

