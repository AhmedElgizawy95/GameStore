using System.ComponentModel.DataAnnotations;
using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



GameStoreData data= new ();

app.MapGetGames(data);

/*app.MapGet("/games", () => data.GetGames().Select(game=>new GameSummaryDto(game.Id,
game.Name,game.Genere.Name,game.Price,game.ReleaseDate)));*/

// GET /games/
app.MapGet("/games/{id}",(Guid id) => {

    Game? game = data.GetGame(id);
    return  game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(game.Id,
    game.Name,
    game.Genere.Id,
    game.Price,
    game.ReleaseDate,
    game.Description
    ));
    
    }).WithName(GetGameEndpointName);


app.MapPost("/games",(CreateGameDto gameDto) => {

var genre = data.GetGenere(gameDto.GenereId);
if(genre is null)
{
    return Results.BadRequest("Invalid Genre");
}
var game = new Game{

    Name=gameDto.Name,
    Genere=genre,
    Price = gameDto.Price,
    ReleaseDate = gameDto.ReleaseDate,
    Description = gameDto.Description
};
    
    data.AddGame(game);

    return Results.CreatedAtRoute(GetGameEndpointName,new {id = game.Id},new GameDetailsDto(game.Id,game.Name,game.Genere.Id,game.Price,game.ReleaseDate,game.Description));
}).WithParameterValidation();


app.MapPut("/games/{id}",(Guid id, UpdateGameDto gameDto) => {

    var  existingGame = data.GetGame(id);
    if(existingGame is null)
    {
        return Results.NotFound();
    }

    var genre = data.GetGenere(id);
    if(genre is null)
    {
        return Results.BadRequest("Invalid Genre");
    }

    existingGame.Name = gameDto.Name;
    existingGame.Genere=genre;
    existingGame.Price = gameDto.Price;
    existingGame.ReleaseDate = gameDto.ReleaseDate;
    existingGame.Description = gameDto.Description;

    return  Results.NoContent();
    }).WithParameterValidation();   

app.MapDelete("/games/{id}",(Guid id) => {
    data.RemoveGame(id);
    return Results.NoContent();
});

app.MapGet("/generes",() => 
data.GetGeneres().Select(genere => new GenereDto(genere.Id , genere.Name)));

app.Run();


public record GenereDto(Guid Id,string Name);

public record CreateGameDto(
[Required]
[StringLength(50)]
string Name,
Guid GenereId,
decimal Price,
DateOnly ReleaseDate,
[Required]
[StringLength(500)]
string Description);

public record UpdateGameDto(
[Required]
[StringLength(50)]
string Name,
Guid GenereId,
decimal Price,
DateOnly ReleaseDate,
[Required]
[StringLength(500)]
string Description);

