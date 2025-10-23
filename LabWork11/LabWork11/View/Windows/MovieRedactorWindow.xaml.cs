using LabWork11.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabWork11.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MovieRedactorWindow.xaml
    /// </summary>
    public partial class MovieRedactorWindow : Window
    {
        public MovieRedactorWindow()
        {
            InitializeComponent();

            DataContext = new MovieRedactorWindowViewModel();
        }

        private void CheckInput(TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
            => Close();

        private void DurationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => CheckInput(e);

        private void YearDatePickerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => CheckInput(e);
    }
}
