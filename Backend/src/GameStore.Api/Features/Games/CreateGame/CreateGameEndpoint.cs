using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;
using GameStore.Api.Shared.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    private const string DefaultImageUri = "https://placehold.co/100";
 public static void MapCreateGame(this IEndpointRouteBuilder app)
    {

        app.MapPost("/", async (
        [FromForm]CreateGameDto gameDto,
        GameStoreContext dbContext,
        //category associated with logger objects
        ILogger<Program> logger,
        FileUploader fileUploader,
        ClaimsPrincipal user) =>
        {

            if (user?.Identity?.IsAuthenticated == false)
            {
                return Results.Unauthorized();
            }

            var currentUserId = user?.FindFirstValue(JwtRegisteredClaimNames.Sub);


            if (string.IsNullOrEmpty(currentUserId))
            {
                return Results.Unauthorized();
            }


            var imageUri = DefaultImageUri;
            if (gameDto.ImageFile is not null)
            {
                var fileUploadResult = await fileUploader.UploadFileAsync(
                    gameDto.ImageFile,
                    StorageNames.GameImagesFolder
                );
                if (!fileUploadResult.IsSuccess)
                {
                    return Results.BadRequest(new { message = fileUploadResult.ErrorMessage });
                }

                imageUri = fileUploadResult.FileUrl;
            }
            var game = new Game
            {

                Name = gameDto.Name,

                GenereId = gameDto.GenereId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                Description = gameDto.Description,
                ImageUri = imageUri!,
                LastUpdatedBy = currentUserId

            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Created Game {GameName} with Price {GamePrice}", game.Name, game.Price);


            //logger.PrintGames();
            return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id },
            new GameDetailsDto(game.Id, game.Name,
            game.GenereId,
            game.Price,
             game.ReleaseDate,
             game.Description,
             game.ImageUri,
             game.LastUpdatedBy));
        }).WithParameterValidation().DisableAntiforgery();

    }
}
