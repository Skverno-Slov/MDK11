using AuthLib.Contexts;
using AuthLib.Models;
using AuthLib.Services;
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

namespace LabWork15Wpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        AuthService _authService;
        CinemaDbContext _context;

        public AuthorizationWindow()
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

        void NavigateToMainWindow()
        {
            var window = new MainWindow();
            Hide();
            window.ShowDialog();
            Show();
        }

        void NavigateToregistrationWindow()
        {
            var window = new RegistrationWindow();
            window.Show();
            Close();
        }

        private void Authorization()
        {
            _authService.Login = LoginTextBox.Text;
            _authService.Password = PasswordBox.Password;
            if (!_authService.IsDataCorrect())
            {
                MessageBox.Show("Логин или пароль не могут быть пустыми.");
                return;
            }

            UserSession.Instance.SetCurrentUser(_authService.AuthorizationUser());

            if (UserSession.Instance.CurrentUser is null)
            {
                MessageBox.Show("Неверный логи или пароль. Или пользователь заблокирован");
                return;
            }

            NavigateToMainWindow();
        }

        private void ToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToregistrationWindow();
        }

        void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Authorization();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }
    }
}
