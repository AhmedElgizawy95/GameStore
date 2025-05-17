using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Generes;
using GameStore.Api.Shared.ErrorHandling;
using GameStore.Api.Shared.FileUpload;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor().AddSingleton<FileUploader>();

var app = builder.Build();


app.UseStaticFiles();

app.MapGames();

app.MapGenres();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();    
}
else 
{
    app.UseExceptionHandler();
}
//app.UseMiddleware<RequestTimingMiddleware>();
app.UseStatusCodePages();

await app.InitializeDbAsync();


app.Run();




