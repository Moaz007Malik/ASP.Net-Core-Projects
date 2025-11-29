namespace UniMgmtSstm.Models
{
    public class Quiz
    {
        public string? Id { get; set; }
        public string Title { get; set; } = "";
        public string Type { get; set; } = "quiz"; // or assignment

        public string? CourseId { get; set; }
        public string? TeacherId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now;

        public string Description { get; set; } = "";
    }

}
