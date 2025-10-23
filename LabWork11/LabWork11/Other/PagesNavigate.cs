using LabWork11.View.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace LabWork11.Other
{
    public static class PagesNavigate
    {
        static readonly Frame? _currentFrame;

        static PagesNavigate()
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame");
            if (frame is not Frame || frame is null)
                throw new NullReferenceException("MainFrame не найден");
            _currentFrame = (Frame)frame;
        }

        public static void NavigateToMovieViewPage()
            => _currentFrame?.Navigate(new MoviePage());
    }
}
