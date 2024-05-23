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
    
    public class RegisterPageViewModel
    {
        private readonly DataService dataService;

        public RegisterPageViewModel()
        {
            dataService = ServiceLocator.Get<DataService>();
        }

        public async void PerformRegister(RegisterDto registerDto)
        {
            try
            {
                await dataService.RegisterUserAsync(registerDto);

                var data = await dataService.LoginUserAsync(new LoginDto {Username = registerDto.Username, Password = registerDto.Password });


                CurrentUser.Instance.User = new User
                {
                    Id = data.UserId,
                    Username = data.UserName,
                    ProfileImageSource = data.UserImage,
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
