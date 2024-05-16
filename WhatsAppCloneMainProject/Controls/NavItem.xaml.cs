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

namespace WhatsAppCloneMainProject.Controls
{
    public partial class NavItem : UserControl
    {
        public NavItem()
        {
            InitializeComponent();
        }



        public ImageBrush Icon
        {
            get { return (ImageBrush)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageBrush), typeof(NavItem), new PropertyMetadata(null));


        public string NavName
        {
            get { return (string)GetValue(NavNameProperty); }
            set { SetValue(NavNameProperty, value); }
        }

        public static readonly DependencyProperty NavNameProperty =
            DependencyProperty.Register("NavName", typeof(string), typeof(NavItem), new PropertyMetadata(null));


        private void navItemEnter(object sender, MouseEventArgs e)
        {
           Border brd = (Border)sender;
           brd.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#404040"));
        }

        private void navItemLeave(object sender, MouseEventArgs e)
        {
            Border brd = (Border)sender;
            brd.Background = new SolidColorBrush(Colors.Transparent);
        }

    }





}
