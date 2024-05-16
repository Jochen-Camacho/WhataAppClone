using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCloneMainProject.Models
{
    public class Contact : INotifyPropertyChanged
    {
        public int ContactID { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string ContactUserId { get; set; }
        public User ContactUser { get; set; }

        private Message lastMessage;

        public Message LastMessage
        {
            get => lastMessage;
            set
            {
                if(lastMessage != value)
                {
                    lastMessage = value;
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }

        public Contact() {
        
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddMessage(Message message)
        {
     
        }
    }
}
