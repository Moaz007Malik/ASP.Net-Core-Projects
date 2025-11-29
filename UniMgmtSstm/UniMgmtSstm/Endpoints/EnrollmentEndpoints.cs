using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/enrollments").RequireAuthorization();

        // Get all enrollments (admin)
        group.MapGet("/", [Authorize(Roles = "faculty")] async (UniversityDbContext db) =>
            await db.Enrollments
                .Include(e => e.Course)
                .Include(e => e.User)
                .ToListAsync());

        // Get student enrollments
        group.MapGet("/student/{studentId}", async (string studentId, UniversityDbContext db) =>
            await db.Enrollments
                .Where(e => e.UserId == studentId)
                .Include(e => e.Course)
                .ToListAsync());

        // Enroll student in course (admin/faculty)
        group.MapPost("/", [Authorize(Roles = "admin,faculty")] async (Enrollment enrollment, UniversityDbContext db) =>
        {
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();
            return Results.Ok("Student enrolled.");
        });

        // Drop student from course
        group.MapDelete("/{id}", [Authorize(Roles = "admin,faculty")] async (string id, UniversityDbContext db) =>
        {
            var enroll = await db.Enrollments.FindAsync(id);
            if (enroll is null) return Results.NotFound();

            db.Enrollments.Remove(enroll);
            await db.SaveChangesAsync();
            return Results.Ok("Student removed from course.");
        });
    }
}
