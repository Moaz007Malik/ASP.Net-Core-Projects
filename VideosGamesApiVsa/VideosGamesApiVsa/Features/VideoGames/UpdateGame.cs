using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideosGamesApiVsa.Data;

namespace VideosGamesApiVsa.Features.VideoGames
{
    public static class UpdateGame
    {
        public record Command(int Id, string Title, string Genre, int ReleaseYear) : IRequest<Response>;
        public record Response(int Id, string Title, string Genre, int ReleaseYear);
        public class Handler(VideoGameDbContext context) : IRequestHandler<Command, Response?>
        {
            public async Task<Response?> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = await context.VideoGames.FindAsync(new object[] { request.Id }, cancellationToken);
                if (videoGame == null)
                {
                    return null;
                }
                videoGame.Title = request.Title;
                videoGame.Genre = request.Genre;
                videoGame.ReleaseYear = request.ReleaseYear;
                await context.SaveChangesAsync(cancellationToken);
                return new Response(videoGame.Id, videoGame.Title, videoGame.Genre, videoGame.ReleaseYear);
            }
        }
    }

    //[ApiController]
    //[Route("api/games")]
    //public class UpdateGameController(ISender sender) : ControllerBase
    //{
    //    [HttpPut("{id}")]
    //    public async Task<ActionResult<UpdateGame.Response>> UpdateGame(int id, [FromBody] UpdateGame.Command command)
    //    {
    //        if (id != command.Id)
    //        {
    //            return BadRequest("ID in the URL does not match ID in the request body.");
    //        }
    //        var response = await sender.Send(command);
    //        if (response == null)
    //        {
    //            return NotFound($"Game with ID {id} not found.");
    //        }
    //        return Ok(response);
    //    }
    //}
}