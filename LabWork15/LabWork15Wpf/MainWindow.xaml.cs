using AuthLib;
using AuthLib.Contexts;
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
        public MainWindow()
        {
            InitializeComponent();
            var context = new CinemaDbContext();
            _context = context;
            var authService = new AuthService(context);
            _authService = authService;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _authService.Login = LoginTextBox.Text;
                _authService.Password = PasswordBox.Password;

                if (!_authService.RegistrateUser())
                {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                    return;
                }
                MessageBox.Show("Пользователь зарегистрирован");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }
    }
}