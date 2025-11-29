using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class CourseRequestEndpoints
{
    public static void MapCourseRequestEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/course-requests").RequireAuthorization();

        // Get all requests (admin/faculty)
        group.MapGet("/", [Authorize(Roles = "admin,faculty")] async (UniversityDbContext db) =>
            await db.CourseRequests.ToListAsync());

        // Get by student
        group.MapGet("/student/{studentId}", async (string studentId, UniversityDbContext db) =>
            await db.CourseRequests.Where(r => r.StudentId == studentId).ToListAsync());

        // Create request (student)
        group.MapPost("/", [Authorize(Roles = "student")] async (CourseRequest request, UniversityDbContext db) =>
        {
            request.Timestamp = DateTime.UtcNow;
            request.Status = "pending";

            db.CourseRequests.Add(request);
            await db.SaveChangesAsync();
            return Results.Ok("Request submitted.");
        });

        // Approve/Reject request (admin/faculty)
        group.MapPut("/{id}", [Authorize(Roles = "admin,faculty")] async (string id, string status, UniversityDbContext db) =>
        {
            var req = await db.CourseRequests.FindAsync(id);
            if (req is null) return Results.NotFound();

            req.Status = status;
            await db.SaveChangesAsync();

            return Results.Ok($"Request {status}.");
        });

        // Delete request (optional)
        group.MapDelete("/{id}", [Authorize(Roles = "admin,faculty")] async (string id, UniversityDbContext db) =>
        {
            var req = await db.CourseRequests.FindAsync(id);
            if (req is null) return Results.NotFound();

            db.CourseRequests.Remove(req);
            await db.SaveChangesAsync();

            return Results.Ok("Request deleted.");
        });
    }
}
