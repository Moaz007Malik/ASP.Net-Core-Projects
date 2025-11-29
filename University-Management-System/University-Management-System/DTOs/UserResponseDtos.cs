namespace University_Management_System.DTOs
{
    public class UserResponseDtos
    {
        public record UserResponseDto(Guid Id, string FullName, string Email, string Role);

        public record UpdateUserDto(string FullName, string Email, string Role);

    }
}
