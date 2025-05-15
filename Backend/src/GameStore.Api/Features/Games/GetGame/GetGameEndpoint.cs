using System;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;
using Microsoft.Data.Sqlite;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{

    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (Guid id, GameStoreContext dbContext) =>
        {

            //Another way for using asynchronous 

            //Task<Game?> findGameTask = dbContext.Games.FindAsync(id).AsTask();
            //    return findGameTask.ContinueWith(task =>
            //    {
            //     Game? game=task.Result;
            //     return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(game.Id,
            //     game.Name,
            //     game.GenereId,
            //     game.Price,
            //     game.ReleaseDate,
            //     game.Description
            //     ));
            //     });
    

            Game? game = await dbContext.Games.FindAsync(id);


            return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(game.Id,
            game.Name,
            game.GenereId,
            game.Price,
            game.ReleaseDate,
            game.Description
            ));



        }).WithName(EndpointNames.GetGame);

    }

}
