using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class ChatViewModel
    {
        public ObservableCollection<Contact> contacts {  get; set; } = new ObservableCollection<Contact>();
        private readonly DataService dataService;

        public ChatViewModel() {

            dataService = ServiceLocator.Get<DataService>();


            PerformGetUserContacts();

        }


        private async void PerformGetUserContacts()
        {
            try
            {
                var data = await dataService.GetUserContacts();
                foreach (Contact contact in data)
                {
                    contacts.Add(contact);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get user contacts: {ex.Message}");
            }
        }
    }
}
