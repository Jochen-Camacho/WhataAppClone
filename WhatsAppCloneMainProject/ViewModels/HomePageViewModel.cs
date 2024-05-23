using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Services;
using WhatsAppCloneMainProject.Utility;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> UnAddedContacts { get; set; } = new ObservableCollection<Contact>();
        public User User { get; set; } 
        public DataService dataService { get; set; }

        public HomePageViewModel()
        {
            dataService = ServiceLocator.Get<DataService>();
            PerformGetUserContacts();
            User = CurrentUser.Instance.User;
        }

        public async void PerformGetUserContacts()
        {
            try
            {
                var data = await dataService.GetUserContacts();

                if (data == null)
                {
                    MessageBox.Show("No data returned from GetUserContacts");
                    return;
                }

                if (data[0] == null || data[1] == null)
                {
                    MessageBox.Show("Contacts data is null");
                    return;
                }

                foreach (Contact contact in data[0])
                {
                    if (!Contacts.Any(c => c.ContactID == contact.ContactID))
                    {
                        Contacts.Add(contact);
                    }
                }
                Contacts.OrderBy(c => c.ContactUser.Username).ToList();

                foreach (Contact contact in data[1])
                {
                    if (!Contacts.Any(c => c.ContactUserId == contact.ContactUserId))
                    {
                        UnAddedContacts.Add(contact);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get user contacts: {ex.Message}");
            }
        }

        public async void PerformUploadUserImage(string imagePath)
        {
            try
            {
                await dataService.UploadImage(imagePath);
                User.ProfileImageSource = ImageUtil.BytesToImageSource(ImageUtil.ImageConverter(imagePath));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to upload image: {ex.Message}");
            }
        }
    }
}
