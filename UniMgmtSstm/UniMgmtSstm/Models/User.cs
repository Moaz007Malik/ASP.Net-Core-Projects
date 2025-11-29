using System.ComponentModel.DataAnnotations.Schema;

namespace UniMgmtSstm.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
        public bool IsAuthToRegister { get; set; }

        [NotMapped]
        public List<Course>? Courses { get; set; }
    }

}
