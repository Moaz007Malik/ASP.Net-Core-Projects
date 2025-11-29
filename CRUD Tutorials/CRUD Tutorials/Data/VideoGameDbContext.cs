using Microsoft.EntityFrameworkCore;

namespace CRUD_Tutorials.Models
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
        public DbSet<VideoGameDetails> VideoGameDetails => Set<VideoGameDetails>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(
                new VideoGame { Id = 1, Title = "Ghost of Yōtei", Platform = "Playstation 5", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 2, Title = "God of War", Platform = "PlayStation 4", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 3, Title = "Halo Infinite", Platform = "Xbox Series X/S", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 4, Title = "Minecraft", Platform = "Multi-platform", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 5, Title = "Final Fantasy VII Remake", Platform = "PlayStation 4", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 6, Title = "Super Mario Odyssey", Platform = "Nintendo Switch", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 7, Title = "The Witcher 3: Wild Hunt", Platform = "Multi-platform", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 8, Title = "Cyberpunk 2077", Platform = "Multi-platform", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 9, Title = "Elden Ring", Platform = "Multi-platform", DeveloperId = null, PublisherId = null },
                new VideoGame { Id = 10, Title = "The Legend of Zelda: Breath of the Wild", Platform = "Nintendo Switch", DeveloperId = null, PublisherId = null }
            );
        }
    }
}
