using Microsoft.EntityFrameworkCore;
using VideosGamesApiVsa.Data;
using VideosGamesApiVsa.Entities;

namespace VideosGamesApiVsa.Endpoints
{
    public static class VideoGameEndpoints
    {
        public static RouteGroupBuilder MapVideoGameEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (VideoGameDbContext context) =>
            await context.VideoGames.ToListAsync());

            group.MapGet("/{id:int}", async (int id, VideoGameDbContext context) =>
            {
                var game = await context.VideoGames.FindAsync(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            });

            group.MapPost("/", async (VideoGameDbContext context, VideoGame game) =>
            {
                if(game is null)
                {
                    return Results.BadRequest("Game cannot be null.");
                }

                context.VideoGames.Add(game);
                await context.SaveChangesAsync();

                return Results.Created($"/api/games/{game.Id}", game);
            });

            group.MapPut("/{id:int}", async (int id, VideoGame updatedGame, VideoGameDbContext context) =>
            {
                var game = await context.VideoGames.FindAsync(id);
                if (game is null)
                {
                    return Results.NotFound($"Game with ID {id} not found.");
                }

                game.Title = updatedGame.Title;
                game.Genre = updatedGame.Genre;
                game.ReleaseYear = updatedGame.ReleaseYear;
                await context.SaveChangesAsync();

                return Results.Ok(game);
            });


            group.MapDelete("/{id:int}", async (int id, VideoGameDbContext context) =>
            {
                var game = await context.VideoGames.FindAsync(id);
                if(game is null)
                {
                    return Results.NotFound($"Game with ID {id} not found.");
                }
                context.VideoGames.Remove(game);
                await context.SaveChangesAsync();
                return Results.NoContent();
            }); 

            return group;
        }
    }
}
