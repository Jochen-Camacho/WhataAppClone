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
using WhatsAppCloneMainProject.Controls;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for Chats.xaml
    /// </summary>
    public partial class Chats : Page
    {
        public ChatViewModel ChatViewModel = new ChatViewModel();

        public Chats()
        {
            InitializeComponent();
            this.DataContext = ChatViewModel;
            //ChatFrame.Content = new ChatContent(ChatViewModel.contacts[0]);
        }

        private void chatItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Contact selectedContact = button.DataContext as Contact;
            ChatFrame.Content = new ChatContent(selectedContact);
        }
    }
}
