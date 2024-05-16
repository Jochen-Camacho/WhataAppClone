using System.ComponentModel.DataAnnotations;

namespace WhatsAppCloneServices.Data.DTOs
{
    public class ContactDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
