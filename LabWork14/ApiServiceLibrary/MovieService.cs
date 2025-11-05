using DataBaseLibrary.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiServiceLibrary
{
    public class MovieService(HttpClient client)
    {
        private readonly HttpClient _client = client;

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var movies = await _client
                .GetFromJsonAsync<List<Movie>>("Movies");
            return movies;
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie = await _client
                .GetFromJsonAsync<Movie>($"Movies/{id}");
            return movie;
        }

        public async Task<Movie> PostMovieAsync(Movie movie)
        {
            using var response = await _client.PostAsJsonAsync("Movies", movie);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Movie>();
        }

        public async Task PutMovieAsync(Movie movie)
        {
            using var response = await _client.PutAsJsonAsync($"Movies/{movie.MovieId}", movie);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMovie(int id)
        {
            using var response = await _client.DeleteAsync($"Movies/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

