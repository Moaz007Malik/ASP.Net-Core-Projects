namespace University_Management_System.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public int Year { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<TimeSlot>? TimeSlots { get; set; }
    }

}
