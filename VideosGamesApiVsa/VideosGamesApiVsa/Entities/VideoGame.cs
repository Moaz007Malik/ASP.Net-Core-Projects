namespace VideosGamesApiVsa.Entities
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
    }
}
