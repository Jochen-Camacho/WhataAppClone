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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Utility;
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPageViewModel RegisterPageViewModel { get; set; } = new RegisterPageViewModel();
        public RegisterPage()
        {
            InitializeComponent();
            this.DataContext = RegisterPageViewModel;
        }

        private void BtnToLogin_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MainWindow;
            window?.NavigateToPage(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterDto registerDto = new RegisterDto
            {
                Username = txbUsername.Text,
                Email = txbEmail.Text,
                Password = txbPassword.Text,
                ProfileImage = ImageUtil.ImageConverter(new Uri("pack://application:,,,/Assets/Placeholders/placeholderImage.jpeg"))
            };

            RegisterPageViewModel.PerformRegister(registerDto);
        }
    }
}
