using Backend_with_Linq_and_Minimal_API.Models;

namespace Backend_with_Linq_and_Minimal_API.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Game> GameLibrary { get; set; } = new();
    }
}
