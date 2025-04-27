namespace GameStore.Api.Features.Games.GetGames;


public record GameSummaryDto(Guid Id, 
string Name, 
string Genere , 
decimal Price , 
DateOnly RealeaseDate);


