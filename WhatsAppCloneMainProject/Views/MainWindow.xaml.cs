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
using WhatsAppCloneMainProject.Controls;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Pages;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly DataService dataService = new DataService();

        public MainWindow()
        {
            InitializeComponent();
            pageFrame.Navigate(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
        }

        public void NavigateToPage(Uri pageUri)
        {
            try
            {
                pageFrame.Navigate(pageUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation to {pageUri} failed: {ex.Message}");
            }
        }

        private async void PerformLoginAsync()
        {
            var check = new LoginDto { Password = "securePassword123!", Username = "newUser123" };

            try
            {
                var data = await dataService.LoginUserAsync(check);
                MessageBox.Show(data.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
            }
        }

        private void GrdTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
