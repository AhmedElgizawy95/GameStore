using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{


public static async Task InitializeDbAsync(this WebApplication app)
{
   await app.MigrateDbAsync();
   await app.SeedDbAsync();
   app.Logger.LogInformation(13,"Database Initialized");


}



 public static async Task MigrateDbAsync(this WebApplication app)
 {
    using var scope = app.Services.CreateScope();
     GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
     
     //updates database automatically
     await dbContext.Database.MigrateAsync();
 }

 public static async Task SeedDbAsync(this WebApplication app)
 {
   using var scope = app.Services.CreateScope();
   GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
 
  
  if(!dbContext.Genres.Any())
  {
   dbContext.AddRange(new Genere 
   {
      Name = "Fighting"
   },
   new Genere 
   {
      Name = "Racing"
   },
   new Genere 
   {
      Name = "Roleplaying"
   },
   new Genere 
   {
      Name = "Sport"
   },
    new Genere 
   {
      Name = "Kids and Family"
   }
   );
   
   
  await dbContext.SaveChangesAsync();
   
  }
  
 }



}
