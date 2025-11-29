using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using University_Management_System.Data;
using University_Management_System.Entities;
using static University_Management_System.DTOs.UserDto;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register", async (RegisterUserDto dto, AppDbContext db) =>
        {
            if (await db.Users.AnyAsync(u => u.Email == dto.Email))
                return Results.BadRequest("Email already exists.");

            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role
            };
            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Results.Ok(new { user.Id, user.Email, user.Role });
        });

        group.MapPost("/login", async (LoginDto dto, AppDbContext db, JwtService jwtService) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Results.NotFound("User not found.");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Results.BadRequest("Invalid credentials.");

            var token = jwtService.GenerateToken(user);
            return Results.Ok(new { token, user.Id, user.Email, user.Role });
        });
    }
}
