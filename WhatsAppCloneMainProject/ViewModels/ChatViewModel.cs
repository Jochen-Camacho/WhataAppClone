using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class ChatViewModel
    {
        public ObservableCollection<Contact> Contacts {  get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> UnAddedContacts {  get; set; } = new ObservableCollection<Contact>();
        private readonly DataService dataService;
        public User SelectedUser { get; set; } = new User();

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ChatViewModel()
        {
            dataService = ServiceLocator.Get<DataService>();
        }

        public async Task InitializeAsync()
        {
            try
            {
                var data = await dataService.GetUserContacts();
                foreach(var contact in data[0])
                {
                    Contacts.Add(contact);
                }

                foreach(var contact in data[1])
                {
                    UnAddedContacts.Add(contact);
                }

                if (UnAddedContacts.Count > 0)
                {
                    IsVisible = true;
                }
            }
            catch   
            (Exception ex)
            { }


        }

        public void SetVisible(bool visible)
        {
            IsVisible = visible;
        }



    }
}
