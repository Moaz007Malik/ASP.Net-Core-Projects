namespace University_Management_System.DTOs
{
    public class UserDto
    {
        public record RegisterUserDto(string FullName, string Email, string Password, string Role);
        public record LoginDto(string Email, string Password);
    }
}

