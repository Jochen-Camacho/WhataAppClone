using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WhatsAppCloneMainProject.Models;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class ChatContentViewModel
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();

        public User User { get; set; }

        public TextBox ChatBox { get; set; }

        public ChatContentViewModel()
        {
            ChatBox = new TextBox();
        }

        public void Initialize(TextBox chatBox)
        {
            ChatBox = chatBox;
        }

        public void SendMessage()
        {
            var newMessage = new Message
            {
                Content = ChatBox.Text,
                Timestamp = DateTime.Now
            };

        }
    }
}
