using System.ComponentModel.DataAnnotations.Schema;

namespace UniMgmtSstm.Models
{
    public class Course
    {
        public string? Id { get; set; }
        public string CourseName { get; set; } = "";
        public string Description { get; set; } = "";
        public string CourseCode { get; set; } = "";

        public List<User>? Teachers { get; set; }
        public List<User>? Students { get; set; }

        [NotMapped]
        public string? TeacherNames { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }

}
