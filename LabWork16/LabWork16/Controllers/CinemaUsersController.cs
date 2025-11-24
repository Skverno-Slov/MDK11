using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Humanizer.Localisation;
using LabWork16.Models;
using LabWork16.Contexts;

namespace LabWork16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaUsersController(CinemaDbContext context) : ControllerBase
    {
        private readonly CinemaDbContext _context = context;

        [HttpGet("{id}")]
        [Authorize(Roles = "Администратор")]
        public async Task<ActionResult<CinemaUser>> GetUserByIdAsync(int id)
        {
           var user = await _context.CinemaUsers.FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null) 
                return NotFound();

            return user;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<CinemaUser>> GetCurrentUserAsync()
        {
            var userId = User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!int.TryParse(userId, out int userIdInt))
                return BadRequest("Id не число");

            var user = await _context.CinemaUsers
                .FirstOrDefaultAsync(u => u.UserId == userIdInt);

            if (user is null)
                return NotFound();

            return user;
        }

            [HttpPost("users")]
        [Authorize(Roles = "Администратор")]
        public async Task<ActionResult<CinemaUser>> PostUserAsync(CinemaUser user)
        {
            await _context.CinemaUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserByIdAsync", new { id = user.UserId }, user);
        }
    }
}
