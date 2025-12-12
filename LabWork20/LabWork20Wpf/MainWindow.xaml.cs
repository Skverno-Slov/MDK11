using LabWork20Lib.Contexts;
using LabWork20Lib.Models;
using LabWork20Lib.Services;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace LabWork20Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CinemaDbContext _context = new();
        MovieService _service;
        string _filePath;
        string _fileName;

        private const long MaxFileSize = 2 * 1024 * 1024;

        public MainWindow()
        {
            InitializeComponent();

            OpenConnection();
            SetItemSource();
        }

        private void SetItemSource()
            => MoviesComboBox.ItemsSource = _service.GetMovies();

        private void OpenConnection()
            => _service = new(_context);

        private void Window_Closed(object sender, EventArgs e)
            => _context.Dispose();

        private void SetMovieFrameButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = SelectImage();
            if (filePath is null)
            {
                MessageBox.Show("Файл не выбран");
                return;
            }
                
            if (filePath.Length > MaxFileSize)
            {
                MessageBox.Show("Файл слишком большой");
                return;
            }

            FileNameTextBlock.Text = filePath;
            _filePath = filePath;
            MessageBox.Show("Файл успешно выбран");
        }

        private string SelectImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Изображения JPEG|*.jpg;*.jpeg| Изображения PNG|*.png| Изображения GIF|*.gif| Все файлы|*.*";

            if (dialog.ShowDialog() is true)
            {
                _fileName = dialog.SafeFileName;
                return dialog.FileName;
            }

            _fileName = null;
            return null;
        }

        private void SaveMovieButton_Click(object sender, RoutedEventArgs e)
        {
            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }

            try
            {
                File.Copy(_filePath, Path.Combine(baseDirectory, _fileName), true);
                MessageBox.Show("Файл Сохранён");

                var movie = MoviesComboBox.SelectedItem as Movie;
                if (movie is null)
                    return;

                _service.SaveFrameImage(movie.MovieId, _fileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}