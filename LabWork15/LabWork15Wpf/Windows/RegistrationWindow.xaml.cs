using AuthLib.Contexts;
using AuthLib.Services;
using LabWork15Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabWork15Wpf
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        AuthService _authService;
        CinemaDbContext _context;
        public RegistrationWindow()
        {
            InitializeComponent();

            OpenConnection();
        }

        private void OpenConnection()
        {
            var context = new CinemaDbContext();
            _context = context;
            var authService = new AuthService(context);
            _authService = authService;
        }

        private void Registration()
        {
            _authService.Login = LoginTextBox.Text;
            _authService.Password = PasswordBox.Password;

            if (!_authService.IsDataCorrect())
            {
                MessageBox.Show("логин или пароль не могут быть пустыми.");
                return;
            }

            if (!_authService.RegistrateUser())
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }
            MessageBox.Show("Пользователь зарегистрирован");
        }

        void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Registration();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ToAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AuthorizationWindow();
            window.Show();
            Close();
        }

        void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }
    }
}
