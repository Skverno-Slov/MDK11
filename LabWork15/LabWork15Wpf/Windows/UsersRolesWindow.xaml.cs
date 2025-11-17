using AuthLib.Contexts;
using AuthLib.Models;
using AuthLib.Services;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для UsersRolesWindow.xaml
    /// </summary>
    public partial class UsersRolesWindow : Window
    {
        AuthService _authService;
        CinemaDbContext _context;

        public UsersRolesWindow()
        {
            InitializeComponent();

            OpenConnection();
            try
            { 
                SetRolesComboBoxItemSource();
                SetUsersDataGriditenSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetUsersDataGriditenSource()
            => UsersDataGrid.ItemsSource = _authService.GetUsers();

        private void SetRolesComboBoxItemSource()
        {
            RolesComboBox.ItemsSource = _authService.GetRoles();

            if (RolesComboBox.ItemsSource is not null )
                RolesComboBox.SelectedIndex = 0;
        }

        private void OpenConnection()
        {
            var context = new CinemaDbContext();
            _context = context;
            var authService = new AuthService(context);
            _authService = authService;
        }

        private void UpdateSeletedUser(string login)
        {
            RolesComboBox.SelectedItem = _authService.GetUserRole();
            LoginTextBlock.Text = login;
        }

        private void SetupAuthService(string login)
        {
            _authService.Login = login;
        }

        private string GetSelectedUserLogin()
        {
            var selectedUser = UsersDataGrid.SelectedItem as CinemaUser;
            if (selectedUser is null)
                return "";

            var login = selectedUser.Login;
            return login;
        }

        private void ChangeRole()
        {
            if (UsersDataGrid.SelectedItem is not null)
            {
                _authService.ChangeUserRoleAsync(UsersDataGrid.SelectedItem as CinemaUser,
                RolesComboBox.SelectedItem as CinemaUserRole);

                MessageBox.Show("Роль изменена");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeRole();
                SetUsersDataGriditenSource();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UsersDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                string login = GetSelectedUserLogin();
                SetupAuthService(login);

                UpdateSeletedUser(login);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
