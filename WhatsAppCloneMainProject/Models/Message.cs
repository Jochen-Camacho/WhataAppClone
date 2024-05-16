using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCloneMainProject.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public  string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public string RecipientUserId { get; set; }
        public User Recipient { get; set; }
    }
}
