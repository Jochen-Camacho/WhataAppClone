using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class AddContactViewModel
    {
        public TextBox Username {  get; set; }
        public TextBox Email { get; set; }

        private readonly DataService dataService;


        public AddContactViewModel()
        {
            Username = new TextBox();
            Email = new TextBox();

            dataService = ServiceLocator.Get<DataService>();
        }

        public void Initialize(TextBox Username, TextBox Email)
        {
            this.Username = Username;
            this.Email = Email;
        }

        public async void PerformAddContact()
        {
            ContactDto contactDto = new ContactDto
            {
                Username = Username.Text,
                Email = Email.Text,
            };

            try
            {
               await dataService.AddContact(contactDto);
               ServiceLocator.Get<HomePageViewModel>().PerformGetUserContacts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Contact Failed to be Added: {ex.Message}");
            }

        }
    }
}
