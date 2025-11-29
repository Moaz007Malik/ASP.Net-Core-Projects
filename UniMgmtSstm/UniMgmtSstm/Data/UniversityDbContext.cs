using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Data;

public class UniversityDbContext(DbContextOptions<UniversityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseRequest> CourseRequests => Set<CourseRequest>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<GradeEntry> GradeEntries => Set<GradeEntry>();
    public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seeding default Admin
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = "3f29bdfa-0a9b-4ad7-9f4f-62d86e3a1e29",
            UserId = "1",
            FullName = "Admin",
            Email = "admin@mail.com",
            Password = "$2a$12$cM8Jv6qvwEdDqAv7h5Fyd./aaB2.tenJVycMj4wk7ZW61dCQz5XzG",
            Role = "faculty",
            IsAuthToRegister = true
        });

        // Relationships

        modelBuilder.Entity<Enrollment>()
    .HasOne(e => e.User)
    .WithMany()
    .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseId);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Teachers)
            .WithMany(); // Simplified many-to-many, override if needed

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithMany();

        modelBuilder.Entity<GradeEntry>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(g => g.StudentId);

        modelBuilder.Entity<GradeEntry>()
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(g => g.CourseId);
    }
}
