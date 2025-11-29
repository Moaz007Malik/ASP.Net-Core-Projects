using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class GradeEndpoints
{
    public static void MapGradeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/grades").RequireAuthorization();

        // Get grades for student
        group.MapGet("/student/{studentId}", async (string studentId, UniversityDbContext db) =>
            await db.GradeEntries
                .Where(g => g.StudentId == studentId)
                .ToListAsync());

        // Get grades for a course
        group.MapGet("/course/{courseId}", async (string courseId, UniversityDbContext db) =>
            await db.GradeEntries
                .Where(g => g.CourseId == courseId)
                .ToListAsync());

        // Add grade (teacher only)
        group.MapPost("/", [Authorize(Roles = "teacher")] async (GradeEntry grade, UniversityDbContext db) =>
        {
            db.GradeEntries.Add(grade);
            await db.SaveChangesAsync();
            return Results.Ok("Grade submitted.");
        });

        // Update grade
        group.MapPut("/{id}", [Authorize(Roles = "teacher")] async (string id, GradeEntry updated, UniversityDbContext db) =>
        {
            var grade = await db.GradeEntries.FindAsync(id);
            if (grade is null) return Results.NotFound();

            grade.Marks = updated.Marks;
            grade.Date = updated.Date;
            grade.Title = updated.Title;
            await db.SaveChangesAsync();

            return Results.Ok("Grade updated.");
        });

        // Delete grade
        group.MapDelete("/{id}", [Authorize(Roles = "teacher")] async (string id, UniversityDbContext db) =>
        {
            var grade = await db.GradeEntries.FindAsync(id);
            if (grade is null) return Results.NotFound();

            db.GradeEntries.Remove(grade);
            await db.SaveChangesAsync();
            return Results.Ok("Grade removed.");
        });
    }
}
