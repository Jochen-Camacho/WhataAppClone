using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCloneMainProject.Models
{
    public class User 
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Message> SentMessages { get; set; }
        public virtual List<Message> ReceivedMessages { get; set; }
        public virtual List<Contact> Contacts { get; set; }

    }
}
