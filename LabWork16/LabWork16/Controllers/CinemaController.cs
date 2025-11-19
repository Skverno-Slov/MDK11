using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthLib.Contexts;
using AuthLib.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Humanizer.Localisation;

namespace LabWork16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController(CinemaDbContext context) : ControllerBase
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
            var user = await _context.CinemaUsers.FirstOrDefaultAsync(u => u.UserId.ToString() == ClaimTypes.NameIdentifier);

            if (user is null)
                return NotFound();

            return user;
        }

        //[HttpGet("movies")]
        //[AllowAnonymous]
        //public async Task<List<Movie>> GetMoviesAsync()
        //    => _context.Movies.ToListAsync();

        //[HttpGet("tickets/{id}")]
        //[Authorize(Roles = "Билетер,Посетитель")]
        //public async Task<ActionResult<Ticket>> GetTicketById(int id)
        //{
        //    var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);

        //    if (ticket is null)
        //        return NotFound();

        //    return ticket;
        //}

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
