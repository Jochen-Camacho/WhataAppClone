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
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private bool isNavShown = false;
        private NavItem selectedItem = new NavItem();
        private readonly DataService dataService;

        public HomePage()
        {
            InitializeComponent();
            dataService = ServiceLocator.Get<DataService>();
            contentFrame.Navigate(new Uri("/Pages/Chats.xaml", UriKind.Relative));
        }




        private void ucMenuIc_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (!isNavShown)
            {
                DoubleAnimation navOpen = new DoubleAnimation
                {
                    From = 45,
                    To = 175,
                    Duration = TimeSpan.FromSeconds(0.2)
                };

                SolidColorBrush brush = navBrd.Background as SolidColorBrush;
                if (brush == null)
                {
                    brush = new SolidColorBrush(Colors.Transparent);
                    navBrd.Background = brush;
                }

                var newColor = (Color)ColorConverter.ConvertFromString("#313131");

                ColorAnimation navColorChange = new ColorAnimation
                {
                    From = brush.Color,
                    To = newColor,
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                navBar.BeginAnimation(StackPanel.WidthProperty, navOpen);
                navBrd.BorderThickness = new Thickness(0, 0, 1, 0);
                navBrd.CornerRadius = new CornerRadius(0, 15, 15, 0);
                navBrd.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D3D3D"));
                brush.BeginAnimation(SolidColorBrush.ColorProperty, navColorChange);

                isNavShown = true;
            }
            else
            {
                DoubleAnimation navClose = new DoubleAnimation
                {
                    From = 175,
                    To = 45,
                    Duration = TimeSpan.FromSeconds(0.2)
                };


                SolidColorBrush brush = navBrd.Background as SolidColorBrush;
                if (brush == null)
                {
                    brush = new SolidColorBrush(Colors.Transparent);
                    navBrd.Background = brush;
                }

                var newColor = (Color)ColorConverter.ConvertFromString("#222222");

                ColorAnimation navColorChange = new ColorAnimation
                {
                    From = brush.Color,
                    To = newColor,
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                navBar.BeginAnimation(StackPanel.WidthProperty, navClose);
                navBrd.BorderThickness = new Thickness(0, 0, 0, 0);
                navBrd.CornerRadius = new CornerRadius(0);
                navBrd.BorderBrush = new SolidColorBrush(Colors.Transparent);
                brush.BeginAnimation(SolidColorBrush.ColorProperty, navColorChange);



                isNavShown = false;
            }
        }

        private void ucChatsBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            contentFrame.Navigate(new Uri("/Pages/Chats.xaml", UriKind.Relative));
            if (selectedItem != null) removePrevSelected();
            selectedItem = sender as NavItem;
            updateSelectedIcon(selectedItem);
        }

        private void updateSelectedIcon(NavItem item)
        {
            item.BorderBrush = new SolidColorBrush(Colors.LightGreen);
        }

        private void removePrevSelected()
        {
            selectedItem.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void ucSettingsBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation settingsOpen = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            SettingMenu.BeginAnimation(Border.HeightProperty, settingsOpen);
        }

        private void btnSettingClose_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation settingsClose = new DoubleAnimation
            {
                From = 200,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            SettingMenu.BeginAnimation(Border.HeightProperty, settingsClose);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
           var mainwin = Application.Current.MainWindow as MainWindow;
           mainwin.NavigateToPage(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
        }
    }
}
