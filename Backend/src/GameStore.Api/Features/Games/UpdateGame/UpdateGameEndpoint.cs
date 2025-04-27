using System;
using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
public static void MapUpdateGame(this IEndpointRouteBuilder app,GameStoreData data)
{
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

}
}
