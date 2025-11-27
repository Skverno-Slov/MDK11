using LabWork25.Contexts;
using LabWork25.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LabWork25.Services
{
    public class CinemaService(CinemaDbContext context)
    {
        CinemaDbContext _context = context;

        public List<SessionDto> GetSessionsByStartDate(DateTime date)
        {
            var sessions = _context.Sessions
                .Include(m => m.Movie)
                .Include(h => h.Hall)
                .Where(s => s.StartDate.Date == date.Date /*&& s.StartDate.Date < date.AddDays(1).Date*/)
                .OrderBy(s => s.Movie.Name)
                .ThenBy(s => s.StartDate)
                .ToList();

            if (sessions is null)
                return null;

            var sessionDtos = new List<SessionDto>();
            foreach (var session in sessions)
            {
                sessionDtos.Add(new SessionDto
                {
                    Name = session.Movie.Name,
                    StartDate = session.StartDate,
                    HallNumber = session.Hall.HallNumber,
                    Price = session.Price,
                });
            }

            return sessionDtos;
        }
    }
}
