using Book_Management_System.Helper;
using Book_Management_System.Models;

namespace Book_Management_System.Endpoints
{
    public static class AuthEndpoints
    {
        private static readonly List<User> Users = new()
    {
        new User { Email = "admin@mail.com", Password = "admin123", Role = "Admin" }
    };

        public static RouteGroupBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/login", (User loginUser, IConfiguration config) =>
            {
                var user = Users.FirstOrDefault(u =>
                    u.Email == loginUser.Email && u.Password == loginUser.Password);

                if (user == null)
                    return Results.Unauthorized();

                var token = JwtHelper.GenerateToken(user.Email, user.Role, config["Jwt:Key"]!);
                return Results.Ok(new { token });
            });

            return group;
        }
    }
}
