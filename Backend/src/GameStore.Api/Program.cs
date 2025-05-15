using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Generes;
using GameStore.Api.Shared.ErrorHandling;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();


var connString = builder.Configuration.GetConnectionString("GameStore");


//Another way to Add the Db Context
//builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlite(connString));

builder.Services.AddSqlite<GameStoreContext>(connString);

//builder.Services.AddScoped<GameDataLogger>();
//builder.Services.AddTransient<GameDataLogger>();
//builder.Services.AddSingleton<GameStoreData>();

builder.Services.AddHttpLogging(options => {
    options.LoggingFields = HttpLoggingFields.RequestMethod| HttpLoggingFields.RequestPath |
                              HttpLoggingFields.ResponseStatusCode | HttpLoggingFields.Duration;
    options.CombineLogs = true;
});

var app = builder.Build();

app.MapGames();

app.MapGenres();

app.UseHttpLogging();
//app.UseMiddleware<RequestTimingMiddleware>();
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

await app.InitializeDbAsync();


app.Run();




