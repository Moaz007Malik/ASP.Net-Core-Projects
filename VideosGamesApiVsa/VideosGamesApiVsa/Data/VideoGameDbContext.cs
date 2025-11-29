using Microsoft.EntityFrameworkCore;

namespace VideosGamesApiVsa.Data
{
    public class VideoGameDbContext : DbContext
    {
        public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.VideoGame> VideoGames { get; set; }
    }
}
