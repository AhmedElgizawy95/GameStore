using System;
using System.Diagnostics;
using GameStore.Api.Data;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndPoint
{
    
public static void MapGetGames(this IEndpointRouteBuilder app)
{
app.MapGet("/", async (GameStoreContext dbContext,
                        [AsParameters] GetGamesDto request) => {

var skipCount = (request.PageNumber - 1) * request.PageSize; 

var filteredGames =  dbContext.Games
                            .Where(game => string.IsNullOrEmpty(request.Name) || EF.Functions.Like(game.Name , $"%{request.Name}%"));
var gamesOnPage = await filteredGames
.OrderBy(game => game.Name)
.Skip(skipCount)
.Take(request.PageSize)
.Include(game=>game.Genere)
.Select(game=>new GameSummaryDto(
    game.Id,
    game.Name,
    game.Genere!.Name,
    game.Price,
    game.ReleaseDate))
    .AsNoTracking().ToListAsync();

    var totalGames = await filteredGames.CountAsync();

    var totalPages = (int)Math.Ceiling(totalGames / (double)request.PageSize);

    return new GamesPageDto(totalPages,gamesOnPage);
    });

}
}
