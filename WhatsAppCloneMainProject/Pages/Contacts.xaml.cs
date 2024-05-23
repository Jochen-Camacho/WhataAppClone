using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WhatsAppCloneMainProject.Controls;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : Page
    {
        public ContactsViewModel ContactsViewModel { get; set; }
        public Contacts(ObservableCollection<Models.Contact> Contacts, ObservableCollection<Models.Contact> UnAddedContacts)
        {
            ContactsViewModel = new ContactsViewModel(Contacts, UnAddedContacts);
            this.DataContext = ContactsViewModel;

            InitializeComponent();
        }
        private void contactItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Contact selectedContact = button.DataContext as Contact;
            ContactFrame.Content = new ContactContent(selectedContact);
        }

        private void BtnAddContactPage_Click(object sender, RoutedEventArgs e)
        {
            ContactFrame.Content = new AddContact();
        }

        private void btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Border brd = sender as Border;
            brd.BorderBrush = new SolidColorBrush(Colors.White);
        }

        private void btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Border brd = sender as Border;
            brd.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }
    }
}
