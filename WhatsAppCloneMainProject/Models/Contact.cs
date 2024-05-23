using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCloneMainProject.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ContactUserId { get; set; }
        public User ContactUser { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
