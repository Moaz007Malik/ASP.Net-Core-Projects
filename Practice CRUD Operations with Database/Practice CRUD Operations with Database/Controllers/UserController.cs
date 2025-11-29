using Microsoft.AspNetCore.Mvc;
using Practice_CRUD_Operations_with_Database.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserDbContext context) : ControllerBase
{
    private readonly UserDbContext _context = context;

    //public UsersController(UserDbContext context)
    //{
    //    _context = context;
    //}

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if(user is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(user);
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        if(user is null)
        {
            return BadRequest("Invalid User Data");
        }
        else
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof (GetUserById), new {id = user.Id}, user);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> PutUser(int id, User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if(user is null)
        {
            return NotFound();
        }
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.UserType = updatedUser.UserType;
        user.UserName = updatedUser.UserName;

        await context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
