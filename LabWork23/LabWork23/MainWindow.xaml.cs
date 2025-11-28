using LabWork23.Contexts;
using LabWork23.Models;
using LabWork23.Service;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace LabWork23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameContext _context = new();
        GameService _service;
        Game _selectedGame;
        string _filePath;
        string _fileName;

        public MainWindow()
        {
            InitializeComponent();

            CreateService();
            SetDataSource();
        }

        private void CreateService()
        {
            _service = new GameService(_context);
        }

        private void SetDataSource()
        {
            GameDataGreed.ItemsSource = _service.GetGames();
        }

        private void SetSelectedGame()
        {
            _selectedGame = GameDataGreed.SelectedItem as Game;
        }

        private void SetSelectedFile(string filePath)
        {
            LogoNameTextBlock.Text = filePath;
            _filePath = filePath;
            MessageBox.Show("Файл выбран");
        }

        private void SaveLogo()
        {
            File.Copy(_filePath, Path.Combine(Environment.CurrentDirectory, "GamesLogo", _fileName), true);
            GameDataGreed.ItemsSource = _service.UpdateGameLogo(_selectedGame, _filePath);
            MessageBox.Show("Успешно сохранено");
        }

        private bool CheckData()
        {
            if (GameDataGreed.SelectedItem is null)
            {
                MessageBox.Show("Выберите игру");
                return false;
            }

            if (_filePath is null)
            {
                MessageBox.Show("Выберите файл");
                return false;
            }

            return true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }

        private void GameDataGreed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetSelectedGame();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() is true)
            {
                string filePath = dialog.FileName;
                _fileName = new FileInfo(filePath).Name;
                try
                {
                    SetSelectedFile(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveLogoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flowControl = CheckData();
                if (!flowControl)
                    return;

                SaveLogo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveScreenshot_Click(object sender, RoutedEventArgs e)
        {
            bool flowControl = CheckData();
            if (!flowControl)
                return;
            //до 2 мб
            byte[] fileBytes = File.ReadAllBytes(_filePath);
        }
    }
}