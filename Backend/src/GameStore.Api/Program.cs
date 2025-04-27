using System.ComponentModel.DataAnnotations;
using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



GameStoreData data= new ();

app.MapGetGames(data);

/*app.MapGet("/games", () => data.GetGames().Select(game=>new GameSummaryDto(game.Id,
game.Name,game.Genere.Name,game.Price,game.ReleaseDate)));*/

// GET /games/
app.MapGetGame(data);

app.MapCreateGame(data);

app.MapUpdateGame(data);


app.MapDelete("/games/{id}",(Guid id) => {
    data.RemoveGame(id);
    return Results.NoContent();
});

app.MapGet("/generes",() => 
data.GetGeneres().Select(genere => new GenereDto(genere.Id , genere.Name)));

app.Run();


public record GenereDto(Guid Id,string Name);


