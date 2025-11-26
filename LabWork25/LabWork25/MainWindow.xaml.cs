using LabWork25.Contexts;
using LabWork25.DTOs;
using LabWork25.Models;
using LabWork25.Services;
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

namespace LabWork25
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CinemaDbContext _context = new();
        CinemaService _service;
        List<SessionDto> _sessions;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            _service = new(_context);
            MovieDatePicker.SelectedDate = DateTime.Now;
            ChangeItemSource();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }

        private void MovieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeItemSource();
        }

        private void ChangeItemSource()
        {
            var date = MovieDatePicker.SelectedDate.Value;
            _sessions = _service.GetSessionsByStartDate(MovieDatePicker.SelectedDate.Value);
            SessionsDataGrid.ItemsSource = _sessions;
        }
    }
}