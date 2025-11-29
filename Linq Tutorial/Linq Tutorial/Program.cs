using Linq_Tutorial;

var games = new List<Game>
{
    new Game { Title = "The Legend of Zelda: Breath of the Wild", Developer = "Nintendo", ReleaseYear = 2017, Rating = 9, Price = 50, Genre = "RPG" },
    new Game { Title = "The Witcher 3: Wild Hunt", Developer = "CD Projekt Red", ReleaseYear = 2015, Rating = 10, Price = 40, Genre = "Action" },
    new Game { Title = "Dark Souls", Developer = "FromSoftware", ReleaseYear = 2011, Rating = 9, Price = 30, Genre = "Action" },
    new Game { Title = "Hollow Knight", Developer = "Team Cherry", ReleaseYear = 2017, Rating = 8, Price = 20, Genre = "Adventure" },
    new Game { Title = "Celeste", Developer = "Maddy Makes Games", ReleaseYear = 2018, Rating = 9, Price = 15, Genre = "Adventure" },
    new Game { Title = "Stardew Valley", Developer = "ConcernedApe", ReleaseYear = 2016, Rating = 9, Price = 10, Genre = "RPG" },
    new Game { Title = "Undertale", Developer = "Toby Fox", ReleaseYear = 2015, Rating = 9, Price = 10, Genre = "RPG" },
    new Game { Title = "Hades", Developer = "Supergiant Games", ReleaseYear = 2020, Rating = 9, Price = 25, Genre = "Action" },
    new Game { Title = "Hollow Knight: Silksong", Developer = "Team Cherry", ReleaseYear = 2023, Rating = 9, Price = 30, Genre = "Adventure" },
    new Game { Title = "Disco Elysium", Developer = "ZA/UM", ReleaseYear = 2019, Rating = 8, Price = 35, Genre = "RPG" }
};

//var allGames = games.Select(g => g.Title);

//var gameDevs = games.Where(g => g.Developer == "Team Cherry").Select(g => g.Title);
//var gameDevs = games.Where(g => g.Developer == "Team Cherry");

//foreach (var game in gameDevs)
//{
//    //Console.WriteLine(game);
//    Console.WriteLine(game.Title);
//}

//var modernGames = games.Any(g => g.ReleaseYear >= 2024);
//Console.WriteLine($"Are there any modern games? {modernGames}");

//var sortByYear = games.OrderBy(g => g.ReleaseYear);
//var sortByYear = games.OrderByDescending(g => g.ReleaseYear);
//foreach (var game in sortByYear)
//{
//       Console.WriteLine($"{game.Title} - {game.ReleaseYear}");
//}

//var averagePrice = games.Average(g => g.Price);
//Console.WriteLine($"Average Price: {averagePrice:C}");

//var maxRating = games.Max(g => g.Rating);
//var bestGame = games.First(g => g.Rating == maxRating);
//Console.WriteLine($"Best Game: {bestGame.Title} with rating of {maxRating}");

//var sortedGames = games.OrderBy(g => g.ReleaseYear);
//var groupGames = sortedGames.GroupBy(g => g.ReleaseYear);
//foreach (var group in groupGames)
//{
//    Console.WriteLine($"Year: {group.Key}");
//    foreach(var game in group)
//    {
//        Console.WriteLine(game.Title);
//    }
//}

//var bestAdventureGame = games.Where(g => g.Genre == "Adventure" && g.Price <= 25).OrderByDescending(g => g.Rating).Select(g => $"{g.Title} - {g.Price} - {g.Rating}");
//foreach (var game in bestAdventureGame)
//{
//    Console.WriteLine(game);
//}

//var paginatedGames = games.Skip(2).Take(3);
//foreach (var game in paginatedGames)
//{
//    Console.WriteLine(game.Title);
//}



//var adventureGames = games.Where(g => g.Genre == "Adventure");
//foreach(var game in adventureGames)
//{
//    Console.WriteLine($"{game.Title} - {game.Developer} - {game.ReleaseYear} - {game.Rating} - {game.Price:C}");
//}

//var adventureGamesQuery = from g in games
//                          where g.Genre == "Adventure"
//                          select g;
//foreach(var game in adventureGamesQuery)
//{
//    Console.WriteLine(game.Title);
//}

//var cheapestGame = games.OrderBy(g => g.Price).First();
//Console.WriteLine($"{cheapestGame.Title} - {cheapestGame.Developer} - {cheapestGame.ReleaseYear} - {cheapestGame.Rating} - {cheapestGame.Price:C}");

//var genres = games.Select(g => g.Genre).Distinct();
//foreach(var genre in genres)
//{
//    Console.WriteLine(genre);
//}
