using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserDbContext _db;

    public UsersController(UserDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _db.Users.Include(u => u.Subscription).ToListAsync();
    }

    [HttpGet("bySubscription/{type}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetBySubscription(string type)
    {
        var users = await _db.Users
            .Include(u => u.Subscription)
            .Where(u => u.Subscription.Type == type)
            .ToListAsync();

        var result = users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            SubscriptionType = u.Subscription?.Type ?? "Unknown"
        });

        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
