using Microsoft.EntityFrameworkCore;

namespace Practice_CRUD_Operations_with_Database.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "admin", Password = "admin123", Email = "admin@gmail.com", UserType = "student" },
                new User { Id = 2, UserName = "Moaz", Password = "moaz123", Email = "moaz007malik@gmail.com", UserType = "student" }
            );
        }
    }
}
