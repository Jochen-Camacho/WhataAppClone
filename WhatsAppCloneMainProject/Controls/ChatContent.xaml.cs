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
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;

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
            this.DataContext = ChatContentViewModel;

            ChatContentViewModel.Initialize(txbChatBox);
            ContactName = contact.ContactUser.Username;
            ChatContentViewModel.User = contact.ContactUser;
            ChatContentViewModel.GetUserMessages();
            var messages = DataContext as ChatContentViewModel;
            if (messages != null)
            {
                ((INotifyCollectionChanged)messages.Messages).CollectionChanged += Messages_CollectionChanged;
            }
        }

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var listView = LvMesgs;
                if (listView.Items.Count > 0)
                {
                    // Use Dispatcher to ensure it runs on the UI thread
                    listView.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        listView.ScrollIntoView(listView.Items[listView.Items.Count - 1]);
                    }));
                }
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
