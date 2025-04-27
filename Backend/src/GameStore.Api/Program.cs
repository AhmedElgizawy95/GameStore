using System.ComponentModel.DataAnnotations;
using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.DeleteGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;
using GameStore.Api.Features.Generes.GetGeneres;
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


app.MapDeleteGame(data);

app.MapGetGenres(data);


app.Run();




