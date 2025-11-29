namespace University_Management_System.Entities
{
    public class TimeSlot
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public Guid TeacherId { get; set; }
        public User? Teacher { get; set; }
    }

}
