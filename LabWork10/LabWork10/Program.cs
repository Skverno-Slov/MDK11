using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using CinemaDbLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
using var context = new AppDbContext(optionsBuilder.Options);

var visitorService = new VisitorService(context);
var ticketService = new TicketService(context);
var movieService = new MovieService(context);
var genreService = new GenreService(context);
try
{
    var visitors = await visitorService.GetVisitorsAsync();
    var visitor = await visitorService.GetVisitorAsync(1);

    var tickets = await ticketService.GetTicketsAsync();
    var ticket = await ticketService.GetTicketAsync(1);

    var movies = await movieService.GetMoviesAsync();
    var movie = await movieService.GetMovieAsync(1);

    var genres = await genreService.GetGenresAsync();
    var genre = await genreService.GetGenreAsync(1);
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}

void ViewList(List<Visitor> visitors)
{
    if (visitors is not null)
    {
        foreach (var visitor in visitors)
        {
            Console.WriteLine($"{visitor.Name} {visitor.Phone} {visitor.Birthday} {visitor.Email}");
        }
    }
}



