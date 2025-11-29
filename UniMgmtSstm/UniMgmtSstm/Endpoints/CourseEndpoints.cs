using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/courses").RequireAuthorization();

        // Get all courses
        group.MapGet("/", async (UniversityDbContext db) =>
            await db.Courses
                .Include(c => c.Teachers)
                .Include(c => c.Students)
                .ToListAsync());

        // Get course by ID
        group.MapGet("/{id}", async (string id, UniversityDbContext db) =>
            await db.Courses
                .Include(c => c.Teachers)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id) is Course course
                    ? Results.Ok(course)
                    : Results.NotFound());

        // Create course (Admin only)
        group.MapPost("/", [Authorize(Roles = "faculty")] async (Course course, UniversityDbContext db) =>
        {
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return Results.Ok(course);
        });

        // Update course (Admin only)
        group.MapPut("/{id}", [Authorize(Roles = "faculty")] async (string id, Course updated, UniversityDbContext db) =>
        {
            var course = await db.Courses.Include(c => c.Teachers).Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == id);
            if (course is null) return Results.NotFound();

            course.CourseName = updated.CourseName;
            course.CourseCode = updated.CourseCode;
            course.Description = updated.Description;

            await db.SaveChangesAsync();
            return Results.Ok(course);
        });

        // Delete course (Admin only)
        group.MapDelete("/{id}", [Authorize(Roles = "faculty")] async (string id, UniversityDbContext db) =>
        {
            var course = await db.Courses.FindAsync(id);
            if (course is null) return Results.NotFound();

            db.Courses.Remove(course);
            await db.SaveChangesAsync();

            return Results.Ok("Course deleted.");
        });
    }
}
