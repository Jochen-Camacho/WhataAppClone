using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace WhatsAppCloneMainProject.Models
{
    public class User : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }
        public byte[] ProfileImage { get; set; }

        private ImageSource profileImageSource;
        public ImageSource ProfileImageSource
        {
            get => profileImageSource;
            set
            {
                if (profileImageSource != value)
                {
                    profileImageSource = value;
                    OnPropertyChanged(nameof(ProfileImageSource));
                }
            }
        }

        public virtual List<Message> SentMessages { get; set; }
        public virtual List<Message> ReceivedMessages { get; set; }
        public virtual List<Contact> Contacts { get; set; }

        private Message lastMessage;
        public Message LastMessage
        {
            get => lastMessage;
            set
            {
                if (lastMessage != value)
                {
                    lastMessage = value;
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
