using System.ComponentModel.DataAnnotations;
using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.DeleteGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;
using GameStore.Api.Features.Generes;
using GameStore.Api.Features.Generes.GetGeneres;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


GameStoreData data= new ();

app.MapGames(data);

app.MapGenres(data);



app.Run();




