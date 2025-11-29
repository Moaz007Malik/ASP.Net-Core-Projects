using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideosGamesApiVsa.Data;

namespace VideosGamesApiVsa.Features.VideoGames
{
    public static class DeleteGame
    {
        public record class Command(int Id) : IRequest<bool>;

        public class Handler(VideoGameDbContext context) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = await context.VideoGames.FindAsync(request.Id);
                if (videoGame == null)
                {
                    return false;
                }
                context.VideoGames.Remove(videoGame);
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
    //[ApiController]
    //[Route("api/games")]
    //public class DeleteGameController(ISender sender) : ControllerBase
    //{
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteGame(int id)
    //    {
    //        var command = new DeleteGame.Command(id);
    //        var result = await sender.Send(command);
    //        if (!result)
    //        {
    //            return NotFound($"Game with ID {id} not found.");
    //        }
    //        return NoContent();
    //    }
    //}
}
