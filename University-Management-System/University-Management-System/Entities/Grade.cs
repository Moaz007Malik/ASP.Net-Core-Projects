namespace University_Management_System.Entities
{
    public class Grade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty; // "Assignment 1"
        public string Type { get; set; } = string.Empty;  // "Assignment" or "Quiz"
        public DateTime Date { get; set; }
        public int Score { get; set; }

        public Guid UserId { get; set; }
        public User? Student { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
    }

}
