using LabWork25.Contexts;
using LabWork25.DTOs;
using LabWork25.Managers;
using LabWork25.Models;
using LabWork25.Services;
using Microsoft.Win32;
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
        SessionManager _manager;
        readonly List<string> _header = ["Название фильма", "Время начала", "Зал", "Цена"];

        public MainWindow()
        {
            InitializeComponent();

            SetProperties();
            ChangeItemSource();
        }

        private void SetProperties()
        {
            DataContext = this;
            _service = new(_context);
            MovieDatePicker.SelectedDate = DateTime.Now;
            _manager = new SessionManager(_header);
        }

        private void ChangeItemSource()
        {
            var date = MovieDatePicker.SelectedDate.Value;
            _sessions = _service.GetSessionsByStartDate(MovieDatePicker.SelectedDate.Value);
            SessionsDataGrid.ItemsSource = _sessions;
        }

        private void MovieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeItemSource();
        }

        private void SaveCsv_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (dialog.ShowDialog() is true)
            {
                string filePath = dialog.FileName;

                try
                {
                    _manager.SaveSessionsCsv(_sessions, filePath);
                    MessageBox.Show("Расписане сохранено");
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveXlsx_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "xlsx files (*.xlsx)|*.xlsx"
            };

            if (dialog.ShowDialog() is true)
            {
                string filePath = dialog.FileName;

                //try
                //{
                    _manager.SaveSessionsXlsx(_sessions, filePath);
                    MessageBox.Show("Расписане сохранено");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }
    }
}