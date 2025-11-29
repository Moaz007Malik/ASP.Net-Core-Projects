namespace UniMgmtSstm.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public string CourseId { get; set; } = null!;

        public DateTime EnrolledAt { get; set; }

        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}