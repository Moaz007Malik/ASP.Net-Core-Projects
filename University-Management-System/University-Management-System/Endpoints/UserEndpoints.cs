using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Data;
using static University_Management_System.DTOs.UserResponseDtos;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/users");

        group.MapGet("/", [Authorize(Roles = "faculty")] async (AppDbContext db) =>
        {
            var users = await db.Users
                .Select(u => new UserResponseDto(u.Id, u.FullName, u.Email, u.Role))
                .ToListAsync();

            return Results.Ok(users);
        });

        group.MapGet("/{id:guid}", [Authorize] async (Guid id, AppDbContext db, HttpContext http) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return Results.NotFound();

            var currentUserId = http.User.FindFirst("sub")?.Value;
            var currentUserRole = http.User.FindFirst("role")?.Value;

            // Only self or faculty can view
            if (user.Id.ToString() != currentUserId && currentUserRole != "faculty")
                return Results.Forbid();

            return Results.Ok(new UserResponseDto(user.Id, user.FullName, user.Email, user.Role));
        });

        group.MapPut("/{id:guid}", [Authorize(Roles = "faculty")] async (Guid id, UpdateUserDto dto, AppDbContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return Results.NotFound();

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Role = dto.Role;

            await db.SaveChangesAsync();
            return Results.Ok(new UserResponseDto(user.Id, user.FullName, user.Email, user.Role));
        });

        group.MapDelete("/{id:guid}", [Authorize(Roles = "faculty")] async (Guid id, AppDbContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return Results.NotFound();

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
