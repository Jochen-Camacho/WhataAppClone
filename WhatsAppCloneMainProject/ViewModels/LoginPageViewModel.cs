using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class LoginPageViewModel
    {
        private readonly DataService dataService;

        public LoginPageViewModel()
        {
            dataService = ServiceLocator.Get<DataService>();
        }

        public async void LoginUser(LoginDto loginDto)
        {
            try
            {
                var data = await dataService.LoginUserAsync(loginDto);

                CurrentUser.Instance.User = new User
                {
                    Id = data.UserId,
                    Username = data.UserName,
                    ProfileImageSource = data.UserImage
                };

                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.NavigateToPage(new Uri("/Pages/HomePage.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
            }
        }
    }
}
