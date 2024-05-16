namespace WhatsAppCloneServices.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public required string UserId { get; set; }  
        public required virtual User User { get; set; } 
        public required string ContactUserId { get; set; } 
        public required virtual User ContactUser { get; set; } 
    }
}
