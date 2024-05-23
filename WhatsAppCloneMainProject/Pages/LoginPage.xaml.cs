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
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly DataService dataService;
        private readonly LoginPageViewModel loginPageViewModel = new LoginPageViewModel();
        public LoginPage()
        {
            InitializeComponent();
            dataService = ServiceLocator.Get<DataService>();
            this.DataContext = loginPageViewModel;
            //AutoLogin();
        }

        private void AutoLogin()
        {
            var loginDto = new LoginDto
            {
                Username = "newUser123",
                Password = "securePassword123!",
            };

            //var loginDto = new LoginDto
            //{
            //    Username = "newUser0",
            //    Password = "securePassword0!",
            //};

            loginPageViewModel.LoginUser(loginDto);
        
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginDto = new LoginDto
            {
                Username = txbUsername.Text,
                Password = txbPassword.Text,
            };

            loginPageViewModel.LoginUser(loginDto);
        }

        private void BtnToRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MainWindow;
            window?.NavigateToPage(new Uri("/Pages/RegisterPage.xaml", UriKind.Relative));
        }
    }
}
