using LabWork12.Contexts;
using LabWork12.Services;
//Scaffold-DbContext "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3114;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -Tables Visitor, Ticket, Movie, Session -ContextDir Contexts -Context DbContext -OutputDir Models

try {
    using var context = new AppDbContext();
    var movieService = new MovieService(context);
    var sessionService = new SessionService(context);
    var genreService = new GenreService(context);

    //Task1
    var movies = await movieService.GetMoviesAsync("Name");
    movies.ForEach(x => Console.WriteLine($"{x.Name} {x.Year}"));

    //Task2
    string? movieName = Console.ReadLine();
    short.TryParse(Console.ReadLine(), out short movieYear);
    movies = await movieService.GetMoviesByTitleAndYearAsync(movieName, movieYear);
    movies.ForEach(x => Console.WriteLine($"{x.Name} {x.Year}"));

    //Task3
    Console.WriteLine(await sessionService.IncreasePricesByHallAsync(1, 100));

    //Task4
    var genres = await genreService.GetGenresByMovieIdAsync(1);
    foreach (var genre in genres)
        Console.WriteLine(genre);

    //Task5
    Console.WriteLine(await sessionService.GetSessionDateTimeByTicketIdAsync(1));

}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}