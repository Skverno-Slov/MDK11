using LabWork11.Commands;
using LabWork11.Contexts;
using LabWork11.Models;
using LabWork11.Other;
using LabWork11.Services;
using LabWork11.View.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LabWork11.ViewModel
{
    public class MoviePageViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Movie> _movies;
        ObservableCollection<Movie> _selectedMovies;
        RelayCommand _addCommand;
        RelayCommand _removeCommand;

        public ObservableCollection<Movie> SelectedMovies
        {
            get => _selectedMovies;
            set
            {
                _selectedMovies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand(async obj =>
                    {
                        try
                        {
                            ObservableCollection<Movie> movies = obj as ObservableCollection<Movie>;
                            if (movies.Count == 0)
                                return;

                            using var context = new AppDbContext();
                            var service = new MovieService(context);

                            if (MessageBox.Show($"Вы уверены, что хотите удалить {movies.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                await service.RemoveMoviesAsync(movies);
                                Movies = ViewMovie();
                                MessageBox.Show("Данные успешно удалены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            };
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось удалить записи.\nПричина:{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    },
                    (obj) => Movies.Count > 0));
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(async obj =>
                  {
                      var window = new MovieRedactorWindow();
                      window.ShowDialog();
                      Movies = await ViewMovieAsync();
                  }));
            }
        }



        public MoviePageViewModel()
        {
            Movies = ViewMovie();
            SelectedMovies = new();
        }

        public ObservableCollection<Movie> ViewMovie()
        {
            try
            {
                using var context = new AppDbContext();
                var service = new MovieService(context);

                return service.GetMovies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<ObservableCollection<Movie>> ViewMovieAsync()
        {
            try
            {
                using var context = new AppDbContext();
                var service = new MovieService(context);

                return await service.GetMoviesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
