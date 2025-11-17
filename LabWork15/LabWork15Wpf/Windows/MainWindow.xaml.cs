using AuthLib.Contexts;
using AuthLib.Models;
using AuthLib.Services;
using LabWork15Wpf.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabWork15Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthService _authService;
        CinemaDbContext _context;
        List<string> _privileges;

        public MainWindow()
        {
            InitializeComponent();

            OpenConnection();
            try
            {
                ShowWelcomeMessage();

                SetupService();
                GetPrivileges();

                LockToPersonalCabinetButton();
                LockToMoviesButton();
                LockToTicketsButton();
                LockAddUserButton();
                LockToUsersButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetPrivileges()
        {
            _privileges = _authService.GetUserPrivileges();
        }

        private void SetupService()
        {
            var currentUser = UserSession.Instance.CurrentUser;
            _authService.Login = currentUser.Login;
            _authService.Password = currentUser.HashPassword;
        }

        private void LockToUsersButton()
            => ToUsersButton.IsEnabled = _privileges.Contains("редактирование пользователей");

        private void LockAddUserButton()
            => AddUserButton.IsEnabled = _privileges.Contains("добавление пользователей");

        private void LockToTicketsButton()
            => ToTicketsButton.IsEnabled = _privileges.Contains("проверка билетов");

        private void LockToMoviesButton()
            => ToMoviesButton.IsEnabled = _privileges.Contains("просмотр списка фильмов");

        private void LockToPersonalCabinetButton()
            => ToPersonalСabinetButton.IsEnabled = _privileges.Contains("доступ в личный кабинет");

        private void OpenConnection()
        {
            var context = new CinemaDbContext();
            _context = context;
            var authService = new AuthService(context);
            _authService = authService;
        }

        private void ShowWelcomeMessage()
        {
            LoginTextBlock.Text = $"Добро пожаловать, {UserSession.Instance.CurrentUser.Login}";
        }

        private static void NavigateToUsersRolesWindow()
        {
            var window = new UsersRolesWindow();
            window.ShowDialog();
        }

        void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.Instance.Clear();
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }

        private void ToUsersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToUsersRolesWindow();
        }
    }
}