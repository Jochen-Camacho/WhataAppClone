using System.ComponentModel.DataAnnotations;

namespace WhatsAppCloneServices.Data.DTOs
{
    public class LoginDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }
    }
}
