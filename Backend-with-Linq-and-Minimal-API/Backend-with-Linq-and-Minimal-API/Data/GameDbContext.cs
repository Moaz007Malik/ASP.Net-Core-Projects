using Backend_with_Linq_and_Minimal_API.Model;
using Backend_with_Linq_and_Minimal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_with_Linq_and_Minimal_API.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
        public DbSet<Player>Players => Set<Player>();
        public DbSet<Game> Games => Set<Game>();
    }
}
