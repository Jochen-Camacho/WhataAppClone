using Microsoft.AspNetCore.Identity;

namespace WhatsAppCloneServices.Models
{
    public class User : IdentityUser
    {
        public required virtual List<Message> SentMessages { get; set; }
        public required virtual List<Message> ReceivedMessages { get; set; }
        public required virtual List<Contact> Contacts { get; set; }

        public User() {
            SentMessages = new List<Message>();
            ReceivedMessages = new List<Message>();
            Contacts = new List<Contact>();
        }

    }
}
