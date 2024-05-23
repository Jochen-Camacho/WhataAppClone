using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WhatsAppCloneMainProject.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public string UserId {  get; set; }
        public string UserName { get; set; }
        public ImageSource UserImage { get; set; }
    }
}
