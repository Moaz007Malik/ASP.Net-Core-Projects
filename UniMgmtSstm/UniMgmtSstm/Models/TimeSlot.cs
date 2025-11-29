namespace UniMgmtSstm.Models
{
    public class TimeSlot
    {
        public string? Id { get; set; }

        public string? CourseId { get; set; }
        public string CourseName { get; set; } = "";
        public string CourseCode { get; set; } = "";

        public string? TeacherId { get; set; }

        public List<string> StudentIds { get; set; } = [];

        public string Rooms { get; set; } = "";
        public string Day { get; set; } = "";
        public string StartTime { get; set; } = "";
        public string EndTime { get; set; } = "";
    }

}
