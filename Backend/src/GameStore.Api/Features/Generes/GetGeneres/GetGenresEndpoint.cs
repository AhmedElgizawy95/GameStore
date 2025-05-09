using System;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Generes.GetGeneres;

public static class GetGenresEndpoint
{
 public static void MapGetGenres(this IEndpointRouteBuilder app)
 {
    app.MapGet("/",async ( GameStoreContext dbContext) => 
await dbContext.Genres.Select(genere => new GenereDto(genere.Id , genere.Name)).AsNoTracking().ToListAsync());

 }
}
