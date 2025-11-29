namespace UniMgmtSstm.Models
{
    public class GradeEntry
    {
        public string? Id { get; set; }

        public string? StudentId { get; set; }
        public string? CourseId { get; set; }

        public string Type { get; set; } = "Quiz"; // or Assignment
        public string Title { get; set; } = "";

        public DateTime Date { get; set; }
        public double Marks { get; set; }
    }

}
