using System;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndPoint
{
public static void MapGetGames(this IEndpointRouteBuilder app)
{
app.MapGet("/", async (GameStoreContext dbContext) => 
await dbContext.Games.Include(game=>game.Genere)
.Select(game=>new GameSummaryDto(
    game.Id,
    game.Name,
    game.Genere!.Name,
    game.Price,
    game.ReleaseDate))
    .AsNoTracking().ToListAsync());

}
}
