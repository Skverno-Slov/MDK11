using ApiServiceLibrary;
using DataBaseLibrary.Models;
using System.Text.Json;

var client = new HttpClient();
string baseUrl = "https://localhost:7159/api/";
client.BaseAddress = new Uri(baseUrl);

MovieService movieService = new(client);

const int MovieId = 20;

//Task 1
try
{
    Console.WriteLine("Task 1 \n Movies");
    var movies = await movieService.GetMoviesAsync();
    Console.WriteLine("Name");
    foreach (var item in movies)
        Console.WriteLine(item.Name, item.MovieId);

    Console.WriteLine($" Movie id = {MovieId}");
    var movie = await movieService.GetMovieAsync(MovieId);
    Console.WriteLine($"Название: {movie.Name}; Id:{movie.MovieId}");

    Console.WriteLine("Созданный объект");
    var newMovie = new Movie()
    {
        Name = "New Movie",
        Duration = 1,
    };
    var createdMovie = await movieService.PostMovieAsync(newMovie);
    Console.WriteLine(createdMovie.Name, createdMovie.MovieId);

    Console.WriteLine("Изменённый фильм");
    movie.Name = "Человек паук, нет пути домой";
    movie.Duration = 80;
    movie.Year = 2021;
    await movieService.PutMovieAsync(movie);
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}
Console.ReadLine();

