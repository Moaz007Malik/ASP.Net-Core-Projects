using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.DTO;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").RequireAuthorization();

        group.MapGet("/", async (string? role, UniversityDbContext db) =>
        {
            var query = db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(role))
                query = query.Where(u => u.Role == role);
            return await query.ToListAsync();
        });

        group.MapPost("/", async (User user, UniversityDbContext db) =>
        {
            try
            {
                if (await db.Users.AnyAsync(u => u.Email == user.Email))
                    return Results.BadRequest("Email already exists.");

                if (await db.Users.AnyAsync(u => u.UserId == user.UserId))
                    return Results.BadRequest("User ID already exists.");

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                db.Users.Add(user);
                await db.SaveChangesAsync();

                return Results.Created($"/api/users/{user.Id}", user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return Results.Problem("An error occurred: " + ex.Message);
            }
        });

        group.MapPut("/{id}", [Authorize(Roles = "faculty")] async (string id, User updatedUser, UniversityDbContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;
            // update other properties as needed

            await db.SaveChangesAsync();
            return Results.Ok(user);
        });

        group.MapPost("/{id}/assign-course", [Authorize(Roles = "faculty")] 
        async (
            string id,
            CourseAssignmentDto dto,
            UniversityDbContext db) =>
        {
            var teacher = await db.Users
            .FirstOrDefaultAsync(u => u.Id == id && u.Role == "teacher");
            var course = await db.Courses.Include(c => c.Teachers)
            .FirstOrDefaultAsync(c => c.Id == dto.CourseId);

            if (teacher == null || course == null)
            return Results.NotFound("Teacher or course not found");

            course.Teachers ??= new List<User>();

            if (!course.Teachers.Any(t => t.Id == teacher.Id))
            course.Teachers.Add(teacher);

            await db.SaveChangesAsync();
            return Results.Ok(course);
        });

        group.MapPost("/{id}/remove-course", [Authorize(Roles = "faculty")] 
        async (
            string id,
            CourseAssignmentDto dto,
            UniversityDbContext db) =>
        {
            var course = await db.Courses.Include(c => c.Teachers).FirstOrDefaultAsync(c => c.Id == dto.CourseId);

            if (course == null)
                return Results.NotFound("Course not found");

            if (course.Teachers != null)
                course.Teachers = course.Teachers.Where(t => t.Id != id).ToList();

            await db.SaveChangesAsync();
            return Results.Ok(course);
        });

        group.MapPost("/{id}/enroll", [Authorize(Roles = "faculty")] 
        async (
                   string id,
                   EnrollmentDto dto,
                   UniversityDbContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            var course = await db.Courses.FindAsync(dto.CourseId);

            if (user == null || course == null)
                return Results.NotFound("User or course not found");

            var enrollment = new Enrollment
            {
                UserId = user.Id,
                CourseId = course.Id ?? string.Empty,
                EnrolledAt = DateTime.UtcNow
            };

            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();

            return Results.Ok("Enrollment successful");
        });

        group.MapGet("/{id}", async (string id, UniversityDbContext db) =>
            await db.Users.FindAsync(id) is User user
                ? Results.Ok(user)
                : Results.NotFound());

        group.MapDelete("/{id}", async (string id, UniversityDbContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.Ok("User deleted");
        });
    }
}
