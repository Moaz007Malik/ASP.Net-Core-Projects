using Book_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_System.Context
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();
    }
}
