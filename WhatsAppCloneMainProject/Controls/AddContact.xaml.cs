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
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Controls
{
    /// <summary>
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : UserControl
    {
        public AddContactViewModel AddContactViewModel { get; set; } = new AddContactViewModel();
        public AddContact()
        {
            InitializeComponent();
            this.DataContext = AddContactViewModel;
            AddContactViewModel.Initialize(txbUsername, txbEmail);
        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            AddContactViewModel.PerformAddContact();
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Border brd = sender as Border; 
            brd.BorderBrush =  new SolidColorBrush(Colors.White);
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Border brd = sender as Border;
            brd.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }
    }
}
