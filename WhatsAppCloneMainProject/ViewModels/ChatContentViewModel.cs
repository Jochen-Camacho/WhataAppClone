using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WhatsAppCloneMainProject.DTOs;
using WhatsAppCloneMainProject.Models;
using WhatsAppCloneMainProject.Services;

namespace WhatsAppCloneMainProject.ViewModels
{
    public class ChatContentViewModel
    {
        public readonly DataService dataService;
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public User User { get; set; }
        public TextBox ChatBox { get; set; }
        public DateTime? LastMessageTimestamp { get; set; } 

        public ChatContentViewModel()
        {
            dataService = ServiceLocator.Get<DataService>();
            ChatBox = new TextBox();
        }

        public void Initialize(TextBox chatBox)
        {
            ChatBox = chatBox;
        }

        public async void SendMessage()
        {
            var newMessage = new MessageDto
            {
                Content = ChatBox.Text,
                RecipientUserId = User.Id,
            };

            try
            {
                await dataService.SendMessage(newMessage);       
                GetUserMessages();
                ChatBox.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send Message: {ex.Message}");
            }
            
        }

        public async void GetUserMessages()
        {
            try
            {
                var data = await dataService.GetUserMessages(new ChatDto
                {
                    RecipientId = User.Id,
                    LastTimeStamp = LastMessageTimestamp,
                });

                foreach (var message in data)
                {
                    if(!Messages.Contains(message)) Messages.Add(message);
                }

                if (data.Any())
                {
                    LastMessageTimestamp = data.Max(m => m.Timestamp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get user messages: {ex.Message}");
            }
        }
    }
}
