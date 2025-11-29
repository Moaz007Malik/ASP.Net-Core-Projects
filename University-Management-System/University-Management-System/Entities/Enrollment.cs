namespace University_Management_System.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
    }

}
