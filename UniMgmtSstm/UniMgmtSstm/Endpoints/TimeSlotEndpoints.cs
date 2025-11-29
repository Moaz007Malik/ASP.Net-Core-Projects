using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class TimeSlotEndpoints
{
    public static void MapTimeSlotEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/timeslots").RequireAuthorization();

        // Get all slots (admin/faculty)
        group.MapGet("/", [Authorize(Roles = "admin,faculty")] async (UniversityDbContext db) =>
            await db.TimeSlots.ToListAsync());

        // Get for a course
        group.MapGet("/course/{courseId}", async (string courseId, UniversityDbContext db) =>
            await db.TimeSlots.Where(t => t.CourseId == courseId).ToListAsync());

        // Get for a teacher
        group.MapGet("/teacher/{teacherId}", async (string teacherId, UniversityDbContext db) =>
            await db.TimeSlots.Where(t => t.TeacherId == teacherId).ToListAsync());

        // Create (admin/faculty only)
        group.MapPost("/", [Authorize(Roles = "admin,faculty")] async (TimeSlot slot, UniversityDbContext db) =>
        {
            db.TimeSlots.Add(slot);
            await db.SaveChangesAsync();
            return Results.Ok("TimeSlot created.");
        });

        // Update
        group.MapPut("/{id}", [Authorize(Roles = "admin,faculty")] async (string id, TimeSlot updated, UniversityDbContext db) =>
        {
            var slot = await db.TimeSlots.FindAsync(id);
            if (slot is null) return Results.NotFound();

            slot.CourseId = updated.CourseId;
            slot.CourseName = updated.CourseName;
            slot.CourseCode = updated.CourseCode;
            slot.TeacherId = updated.TeacherId;
            slot.StudentIds = updated.StudentIds;
            slot.Rooms = updated.Rooms;
            slot.Day = updated.Day;
            slot.StartTime = updated.StartTime;
            slot.EndTime = updated.EndTime;

            await db.SaveChangesAsync();
            return Results.Ok("TimeSlot updated.");
        });

        // Delete
        group.MapDelete("/{id}", [Authorize(Roles = "admin,faculty")] async (string id, UniversityDbContext db) =>
        {
            var slot = await db.TimeSlots.FindAsync(id);
            if (slot is null) return Results.NotFound();

            db.TimeSlots.Remove(slot);
            await db.SaveChangesAsync();
            return Results.Ok("TimeSlot deleted.");
        });
    }
}
