using System;
using GameStore.Api.Data;

namespace GameStore.Api.Features.Generes.GetGeneres;

public static class GetGenresEndpoint
{
 public static void MapGetGenres(this IEndpointRouteBuilder app)
 {
    app.MapGet("/",( GameStoreData data) => 
data.GetGeneres().Select(genere => new GenereDto(genere.Id , genere.Name)));

 }
}
