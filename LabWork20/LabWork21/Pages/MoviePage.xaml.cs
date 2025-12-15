using LabWork20Lib.Contexts;
using LabWork20Lib.Models;
using LabWork20Lib.Services;
using System.Collections.ObjectModel;

namespace LabWork21.Pages;

public partial class MoviePage : ContentPage
{
    private int _currentPage = 1;

    const int PageSize = 2;

    public ObservableCollection<Movie> Movies { get; set; } = new();

	CinemaDbContext _context = new();
	MovieService _movieService;

    public MoviePage()
    {
        InitializeComponent();

        BindingContext = this;

        _movieService = new(_context);

        UpdateMovies(_movieService.GetMovies());
    }

    private void UpdateMovies(List<Movie> movies)
    {
        Movies.Clear();

        movies = AddSort(movies);
        movies = AddFilterByName(movies);
        movies = AddPagination(movies);

        foreach (var movie in movies)
            Movies.Add(movie);
    }

    private List<Movie> AddPagination(List<Movie> movies)
    {
        //Block Buttons

        movies = movies.Skip((_currentPage - 1) * PageSize).ToList();

        return movies;
    }

    private List<Movie> AddFilterByName(List<Movie> movies)
    {
        var input = MovieNameEntry.Text;

        if (String.IsNullOrWhiteSpace(input))
        {
            movies = _context.Movies.ToList();
            movies = AddSort(movies);
            return movies;
        }
            
        movies = movies.Where(m => m.Name.Contains(input)).ToList();
        return movies;
    }

    private List<Movie> AddSort(List<Movie> movies)
    {
        switch (SortPicker.SelectedIndex)
        {
            case 0:
                movies = SortByName(movies);
                break;
            case 1:
                movies = SortByDuration(movies);
                break;
            case 2:
                movies = SortByYear(movies);
                break;
            case 3:
                movies = SortByYearDescending(movies);
                break;
            default:
                break;
        }

        return movies;
    }

    private static List<Movie> SortByYearDescending(List<Movie> movies) 
        => movies = movies.OrderByDescending(m => m.Year).ToList();
        

    private static List<Movie> SortByYear(List<Movie> movies)
        => movies = movies.OrderBy(m => m.Year).ToList();

    private static List<Movie> SortByDuration(List<Movie> movies)
        => movies = movies.OrderBy(m => m.Duration).ToList();

    private static List<Movie> SortByName(List<Movie> movies)
        => movies = movies.OrderBy(m => m.Name).ToList();

    private void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var movies = Movies.ToList();

        UpdateMovies(movies);
    }

    private void MovieNameEntry_Completed(object sender, EventArgs e)
    {
        var movies = Movies.ToList();

        UpdateMovies(movies);
    }
}