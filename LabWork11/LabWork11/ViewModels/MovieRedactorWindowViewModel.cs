using LabWork11.Commands;
using LabWork11.Contexts;
using LabWork11.Models;
using LabWork11.Other;
using LabWork11.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabWork11.ViewModels
{
    public class MovieRedactorWindowViewModel : INotifyPropertyChanged
    {
        ObservableCollection<string> _ageRatings;
        Movie _movie;
        RelayCommand _saveCommand;

        public ObservableCollection<string> AgeRatings
        {
            get => _ageRatings;
            set
            {
                _ageRatings = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _movie.Name;
            set
            {
                _movie.Name = value.Trim();
                OnPropertyChanged();
            }
        }

        public short Duration
        {
            get => _movie.Duration;
            set
            {
                _movie.Duration = (short)value;
                OnPropertyChanged();
            }
        }

        public short Year
        {
            get => _movie.Year;
            set
            {
                _movie.Year = (short)value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _movie.Description;
            set
            {
                _movie.Description = value.Trim();
                OnPropertyChanged();
            }
        }
        public string AgeRating
        {
            get => _movie.AgeRating;
            set
            {
                _movie.AgeRating = value.Trim();
                OnPropertyChanged();
            }
        }
        public DateOnly? ReleaseBegin
        {
            get => _movie.ReleaseBegin;
            set
            {
                _movie.ReleaseBegin = (DateOnly?)value;
                OnPropertyChanged();
            }
        }
        public DateOnly? DistributionEnd
        {
            get => _movie.DistributionEnd;
            set
            {
                _movie.DistributionEnd = (DateOnly?)value;
                OnPropertyChanged();
            }
        }
        public Movie Movie
        {
            get => _movie;
            set
            {
                _movie = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new RelayCommand(async obj =>
                  {
                      try
                      {
                          using var context = new AppDbContext();
                          var service = new MovieService(context);

                          var movie = obj as Movie;

                          await service.AddMovieAsync(movie);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        public MovieRedactorWindowViewModel()
        {
            Movie = new();
            CreateAgeRatingsAsync();
        }

        private void CreateAgeRatingsAsync()
        {
            try
            {
                using var context = new AppDbContext();
                var service = new MovieService(context);

                var ageRatings = service.GetAgeRaitings();
                ageRatings.Add("3+");
                ageRatings.Add("7+");

                AgeRatings = ageRatings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
