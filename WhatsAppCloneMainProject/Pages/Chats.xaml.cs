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
using WhatsAppCloneMainProject.Services;
using WhatsAppCloneMainProject.ViewModels;

namespace WhatsAppCloneMainProject.Pages
{
    /// <summary>
    /// Interaction logic for Chats.xaml
    /// </summary>
    public partial class Chats : Page
    {
        public ChatViewModel ChatViewModel;
        private ObservableCollection<Contact> contactsUnsearched = new ObservableCollection<Contact>();
        private ObservableCollection<Contact> unAddedContactsUnsearched = new ObservableCollection<Contact>();

        public Chats(ObservableCollection<Contact> Contacts, ObservableCollection<Contact> UnAddedContacts)
        {
            InitializeComponent();
            ChatViewModel = new ChatViewModel();
            this.DataContext = ChatViewModel;
            ServiceLocator.Register(ChatViewModel);

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await ChatViewModel.InitializeAsync();

            contactsUnsearched = new ObservableCollection<Contact>(ChatViewModel.Contacts.Select(contact => new Contact
            {
                ContactUser = contact.ContactUser,
                ContactID = contact.ContactID,
                ContactUserId = contact.ContactUserId,
                DateAdded = contact.DateAdded,
                User = contact.User,
                UserId = contact.UserId,
            }));

            unAddedContactsUnsearched = new ObservableCollection<Contact>(ChatViewModel.UnAddedContacts.Select(contact => new Contact
            {
                ContactUser = contact.ContactUser,
                ContactID = contact.ContactID,
                ContactUserId = contact.ContactUserId,
                DateAdded = contact.DateAdded,
                User = contact.User,
                UserId = contact.UserId,
            }));
        }

        private void chatItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Contact selectedContact = button.DataContext as Contact;
            ChatViewModel.SelectedUser = selectedContact.ContactUser;
            ChatFrame.Content = new ChatContent(selectedContact);
        }
        private void TxBChatSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (!contactsUnsearched.Any())
            {
                contactsUnsearched = new ObservableCollection<Contact>(ChatViewModel.Contacts.Select(contact => new Contact
                {
                    ContactUser = contact.ContactUser,
                    ContactID = contact.ContactID,
                    ContactUserId = contact.ContactUserId,
                    DateAdded = contact.DateAdded,
                    User = contact.User,
                    UserId = contact.UserId,
                }));
            }

            if (!unAddedContactsUnsearched.Any())
            {
                unAddedContactsUnsearched = new ObservableCollection<Contact>(ChatViewModel.UnAddedContacts.Select(contact => new Contact
                {
                    ContactUser = contact.ContactUser,
                    ContactID = contact.ContactID,
                    ContactUserId = contact.ContactUserId,
                    DateAdded = contact.DateAdded,
                    User = contact.User,
                    UserId = contact.UserId,
                }));
            }
            if (e.Key == Key.Enter)
            {
                var searchText = TxBChatSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    ChatViewModel.Contacts.Clear();
                    foreach (var contact in contactsUnsearched)
                    {
                        ChatViewModel.Contacts.Add(contact);
                    }

                    ChatViewModel.UnAddedContacts.Clear();
                    foreach (var contact in unAddedContactsUnsearched)
                    {
                        ChatViewModel.UnAddedContacts.Add(contact);
                    }
                }
                else
                {
                    var filteredContacts = contactsUnsearched
                        .Where(c => c.ContactUser.Username.Contains(searchText))
                        .ToList();

                    ChatViewModel.Contacts.Clear();
                    foreach (var contact in filteredContacts)
                    {
                        ChatViewModel.Contacts.Add(contact);
                    }

                    var filteredUnAddedContacts = unAddedContactsUnsearched
                        .Where(c => c.ContactUser.Username.Contains(searchText))
                        .ToList();

                    ChatViewModel.UnAddedContacts.Clear();
                    foreach (var contact in filteredUnAddedContacts)
                    {
                        ChatViewModel.UnAddedContacts.Add(contact);
                    }
                }
            }

            if(ChatViewModel.UnAddedContacts.Count < 1)
            {
                ChatViewModel.SetVisible(false);
            }
            else
            {
                ChatViewModel.SetVisible(true);
            }
        }
    }
}
