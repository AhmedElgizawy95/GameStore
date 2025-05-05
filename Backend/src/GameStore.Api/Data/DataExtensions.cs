using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{


public static void InitializeDb(this WebApplication app)
{
   app.MigrateDb();
   app.SeedDb();

}



 public static void MigrateDb(this WebApplication app)
 {
    using var scope = app.Services.CreateScope();
     GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
     
     //updates database automatically
     dbContext.Database.Migrate();
 }

 public static void SeedDb(this WebApplication app)
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
   
   
   dbContext.SaveChanges();
   
  }
  
 }



}
