using System;
using GameStore.Api.Data;
using GameStore.Api.Features.Generes.GetGeneres;

namespace GameStore.Api.Features.Generes;

public static class GenresEndpoints
{
 public static void MapGenres(this IEndpointRouteBuilder app)
 {
    var group = app.MapGroup("/generes");
    group.MapGetGenres();
 }
}
