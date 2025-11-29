using CRUD_Tutorials.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Tutorials.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase
    {
        private readonly VideoGameDbContext _context = context;

        //public VideoGameController(VideoGameDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAll()
        {
            return Ok(await _context.VideoGames
                .Include(g => g.VideoGameDetails)
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.Genres)
                .ToListAsync());
        }

        //[HttpGet]
        //[Route("{id}")]

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(game);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> PostVideoGame(VideoGame newGame)
        {
            if (newGame == null)
            {
                return BadRequest("Invalid game data.");
            }
            else
            {
                context.VideoGames.Add(newGame);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updatedGame)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }
            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Developer = updatedGame.Developer;
            game.Publisher = updatedGame.Publisher;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }

            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
