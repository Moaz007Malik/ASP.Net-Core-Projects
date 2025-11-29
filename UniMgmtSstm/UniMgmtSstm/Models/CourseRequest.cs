namespace UniMgmtSstm.Models
{
    public class CourseRequest
    {
        public string? Id { get; set; }

        public string? StudentId { get; set; }
        public string StudentName { get; set; } = "";

        public string? CourseId { get; set; }
        public string CourseName { get; set; } = "";

        public string Type { get; set; } = "enroll"; // or "drop"
        public string Status { get; set; } = "pending";

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

}
