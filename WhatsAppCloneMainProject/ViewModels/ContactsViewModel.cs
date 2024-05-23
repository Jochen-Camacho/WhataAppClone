using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppCloneMainProject.Models;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class ContactsViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> UnAddedContacts { get; set; } = new ObservableCollection<Contact>();

        public ContactsViewModel(ObservableCollection<Contact> _contacts, ObservableCollection<Contact> _unAddedContacts)
        {
            Contacts = _contacts;
            UnAddedContacts = _unAddedContacts;
        }
    }
}
