using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCloneMainProject.Models
{
    public class CurrentUser
    {
        private static CurrentUser _instance;

        public static CurrentUser Instance => _instance == null ? _instance = new CurrentUser() : _instance;

        public User User { get; set; }

        private CurrentUser() { }
    }
}
