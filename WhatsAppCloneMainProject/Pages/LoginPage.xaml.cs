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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly DataService dataService;
        public LoginPage()
        {
            InitializeComponent();
            dataService = ServiceLocator.Get<DataService>();
            autoLogin();
        }

        private async void autoLogin()
        {
            var loginDto = new LoginDto
            {
                Username = "newUser123",
                Password = "securePassword123!",
            };

            try
            {
                var data = await dataService.LoginUserAsync(loginDto);

                CurrentUser.Instance.User = new User
                {
                    Id = data.UserId,
                    Username = data.UserName,
                };

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.NavigateToPage(new Uri("/Pages/HomePage.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginDto = new LoginDto
            {
                Username = txbUsername.Text,
                Password = txbPassword.Text,
            };

            try
            {
                var data = await dataService.LoginUserAsync(loginDto);
                MessageBox.Show(data.Message);

                CurrentUser.Instance.User = new User
                {
                    Id = data.UserId,
                    Username = data.UserName,
                };

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.NavigateToPage(new Uri("/Pages/HomePage.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
            }
        }
    }
}
