using System;
using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
private readonly List<Genere> generes= 
[
new Genere{Id = new Guid("eb0d3649-e0cc-4b7b-802c-5b5e7a6ec61e"),Name="Fighting"},
new Genere{Id = new Guid("cbb77890-c040-4e1b-84ee-c7e3cf534898"),Name="Kids and Family"},
new Genere{Id = new Guid("e996694f-92f9-4a78-9341-523bcc95d98c"),Name="Racing"},
new Genere{Id = new Guid("b18464a9-d5d0-4a82-ac69-e0d4de7348a4"),Name="Roleplaying"},
new Genere{Id = new Guid("92ee68d7-73e3-4f9c-a184-5fa8a2478499"),Name="Sport"}
];


private readonly List<Game> games;

public GameStoreData()
{
    games =[
    new Game 
{
    Id = Guid.NewGuid(),
    Name = "Street Fighter II",
    Genere = generes[0],
    Price=19.99m,
    ReleaseDate = new DateOnly(1992,7,15),
    Description = "Street Fighter 2 , the most iconic fighting game of all time"
},
new Game 
{
    Id = Guid.NewGuid(),
    Name = "Final Fantasy XIV",
    Genere = generes[3],
    Price=59.99m,
    ReleaseDate = new DateOnly(2010,9,30),
    Description = "Final Fatanasy Most Epic game"
},
new Game 
{
    Id = Guid.NewGuid(),
    Name = "FIFA 23",
    Genere = generes[4],
    Price=69.99m,
    ReleaseDate = new DateOnly(2022,9,27),
    Description ="Most 5ara game"
}
]; 
}

public IEnumerable<Game> GetGames() => games; //return games

public Game? GetGame(Guid id) => games.Find(game=>game.Id == id);

public void AddGame(Game game)
{
    game.Id = Guid.NewGuid();
    games.Add(game);

}

public void RemoveGame(Guid id)
{
    games.RemoveAll(game=>game.Id == id);
}

public IEnumerable<Genere> GetGeneres() => generes;

public Genere? GetGenere(Guid id) => generes.Find(genre=>genre.Id == id);
}
