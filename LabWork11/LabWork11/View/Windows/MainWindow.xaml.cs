using LabWork11.Other;
using System.Windows;

namespace LabWork11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PagesNavigate.NavigateToMovieViewPage();
        }
    }
}