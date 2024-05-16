using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WhatsAppCloneMainProject.Models;

namespace WhatsAppCloneMainProject.Templates
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CurrentUserTemplate { get; set; }
        public DataTemplate OtherUserTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var message = item as Message;
            if (message == null)
                return null;

            //var currentUser = CurrentUser.Instance.Sender;
            //if (currentUser != null && message.SenderId == currentUser.Id)

            var currentUser = CurrentUser.Instance.User;
            if (CurrentUser.Instance.User != null && message.SenderId == currentUser.Id)
            {
                return CurrentUserTemplate;
            }
            else
            {
                return OtherUserTemplate;
            }
        }
    }
}
