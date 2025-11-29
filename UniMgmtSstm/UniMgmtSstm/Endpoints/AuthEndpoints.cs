using Microsoft.EntityFrameworkCore;
using UniMgmtSstm.Data;
using UniMgmtSstm.Models;
using UniMgmtSstm.Services;

namespace UniMgmtSstm.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register", async (User user, UniversityDbContext db) =>
        {
            if (await db.Users.AnyAsync(u => u.Email == user.Email))
                return Results.BadRequest("Email already exists.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Results.Ok("User registered.");
        });

        group.MapPost("/login", async (User loginUser, UniversityDbContext db, TokenService tokenService) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
                return Results.BadRequest("Invalid credentials.");

            var token = tokenService.CreateToken(user);

            return Results.Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.Role,
                    user.IsAuthToRegister
                }
            });
        });

    }
}
