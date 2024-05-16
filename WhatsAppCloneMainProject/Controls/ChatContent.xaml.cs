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
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.ViewModels;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.Controls
{
    /// <summary>
    /// Interaction logic for ChatContent.xaml
    /// </summary>
    public partial class ChatContent : UserControl
    {
        public ChatContentViewModel ChatContentViewModel { get; set; } = new ChatContentViewModel();
        private readonly DataService dataService;

        public ChatContent(Contact contact)
        {
            InitializeComponent();
            dataService = ServiceLocator.Get<DataService>();
            //ChatContentViewModel.Messages = new ObservableCollection<Message>(contact.ContactUser.Messages);
            this.DataContext = ChatContentViewModel;
            ChatContentViewModel.Initialize(txbChatBox);
            ContactName = contact.ContactUser.Username;
            ChatContentViewModel.User = contact.ContactUser;
            PerformGetUserMessages();

        }

        private async void PerformGetUserMessages()
        {
            try
            {
                if (CurrentUser.Instance?.User?.Id == null)
                {
                    MessageBox.Show("Current user ID is not set.");
                    return;
                }

                if (ChatContentViewModel?.User?.Id == null)
                {
                    MessageBox.Show("Chat user ID is not set.");
                    return;
                }

                var data = await dataService.GetUserMessages(ChatContentViewModel.User.Id);
                foreach (var message in data)
                {
                    ChatContentViewModel.Messages.Add(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get user messages: {ex.Message}");
            }
        }


        public string ContactName
        {
            get { return (string)GetValue(ContactNameProperty); }
            set { SetValue(ContactNameProperty, value); }
        }

        public static readonly DependencyProperty ContactNameProperty =
            DependencyProperty.Register("ContactName", typeof(string), typeof(ChatContent), new PropertyMetadata(null));


        private void txbChatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter ) {
                if(!string.IsNullOrEmpty(ChatContentViewModel.ChatBox.Text.Trim())) ChatContentViewModel.SendMessage();
            }
        }

        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ChatContentViewModel.ChatBox.Text.Trim())) ChatContentViewModel.SendMessage();
        }
    }
}
