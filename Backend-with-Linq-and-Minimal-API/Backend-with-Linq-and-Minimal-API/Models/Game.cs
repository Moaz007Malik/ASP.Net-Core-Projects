namespace Backend_with_Linq_and_Minimal_API.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public int PlayerId { get; set; }
    }
}
