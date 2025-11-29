using Backend_with_Linq_and_Minimal_API.Data;
using Backend_with_Linq_and_Minimal_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend_with_Linq_and_Minimal_API.MinimalAPI
{
    public static class APIs
    {
        public static void MapPlayerEndpoints(WebApplication app)
        {
            app.MapGet("/players", async (GameDbContext db) =>
            {
                return await db.Players.Include(p => p.GameLibrary).ToListAsync();
            });

            app.MapGet("players/{id}", async (int id, GameDbContext db) =>
            await db.Players.Include(p => p.GameLibrary).FirstOrDefaultAsync(p => p.Id == id) is Player player ? Results.Ok(player) : Results.NotFound());

            app.MapPost("/players", async (Player player, GameDbContext db) =>
            {
                db.Players.Add(player);
                await db.SaveChangesAsync();
                foreach (var game in player.GameLibrary)
                    game.PlayerId = player.Id;
                await db.SaveChangesAsync();
                return Results.Created($"/players/{player.Id}", player);
            });

            app.MapPut("/players/{id}", async (int id, Player updatedPlayer, GameDbContext db) =>
            {
                var existingPlayer = await db.Players.Include(p => p.GameLibrary).FirstOrDefaultAsync(p => p.Id == id);
                if (existingPlayer is null) return Results.NotFound();
                existingPlayer.Name = updatedPlayer.Name;
                db.Games.RemoveRange(existingPlayer.GameLibrary);
                foreach (var game in updatedPlayer.GameLibrary)
                    game.PlayerId = id;
                existingPlayer.GameLibrary = updatedPlayer.GameLibrary;
                await db.SaveChangesAsync();
                return Results.Ok(existingPlayer);
            });

            app.MapDelete("/players/{id}", async (int id, GameDbContext db) =>
            {
                var player = await db.Players.Include(p => p.GameLibrary).FirstOrDefaultAsync(p => p.Id == id);
                if (player is null) return Results.NotFound();
                db.Players.Remove(player);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}