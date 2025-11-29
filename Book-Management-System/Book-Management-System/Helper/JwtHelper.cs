using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Book_Management_System.Helper
{
    public static class JwtHelper
    {
        public static string GenerateToken(string email, string role, string secretKey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "book-management-system",
                audience: "book-management-system",
                claims: new[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
                },
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
