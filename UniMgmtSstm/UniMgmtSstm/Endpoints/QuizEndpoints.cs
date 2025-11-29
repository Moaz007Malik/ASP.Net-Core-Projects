using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;

namespace UniMgmtSstm.Endpoints;

public static class QuizEndpoints
{
    public static void MapQuizEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/quizzes").RequireAuthorization();

        // Get all quizzes for a course
        group.MapGet("/course/{courseId}", async (string courseId, UniversityDbContext db) =>
            await db.Quizzes
                .Where(q => q.CourseId == courseId)
                .ToListAsync());

        // Get all quizzes created by a teacher
        group.MapGet("/teacher/{teacherId}", async (string teacherId, UniversityDbContext db) =>
            await db.Quizzes
                .Where(q => q.TeacherId == teacherId)
                .ToListAsync());

        // Create quiz/assignment (teacher only)
        group.MapPost("/", [Authorize(Roles = "teacher")] async (Quiz quiz, UniversityDbContext db) =>
        {
            quiz.CreatedAt = DateTime.UtcNow;
            db.Quizzes.Add(quiz);
            await db.SaveChangesAsync();
            return Results.Ok(quiz);
        });

        // Delete quiz
        group.MapDelete("/{id}", [Authorize(Roles = "teacher")] async (string id, UniversityDbContext db) =>
        {
            var quiz = await db.Quizzes.FindAsync(id);
            if (quiz is null) return Results.NotFound();

            db.Quizzes.Remove(quiz);
            await db.SaveChangesAsync();
            return Results.Ok("Quiz deleted.");
        });
    }
}
