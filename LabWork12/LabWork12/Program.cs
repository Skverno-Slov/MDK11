using LabWork12.Contexts;
using LabWork12.Services;

try
{

    using var context = new AppDbContext();
    var movieService = new MovieService(context);
    var sessionService = new SessionService(context);

    Console.WriteLine("Task1");
    var movies = await movieService.GetMoviesAsync("Name");
    movies.ForEach(x => Console.WriteLine($"{x.Name} {x.Year}"));

    Console.WriteLine("Task2");
    Console.WriteLine("Введите название фильма");
    string? movieName = Console.ReadLine();
    Console.WriteLine("Введите год выхода фильма");
    short.TryParse(Console.ReadLine(), out short movieYear);
    movies = await movieService.GetMoviesByNameAndYearAsync(movieName, movieYear);
    movies.ForEach(x => Console.WriteLine($"{x.Name} {x.Year}"));

    Console.WriteLine("Task3");
    Console.WriteLine(await sessionService.IncreasePricesByHallIdAsync(1, 100));

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}