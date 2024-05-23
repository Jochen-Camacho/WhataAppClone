namespace WhatsAppCloneServices.Data.DTOs
{
    public class ChatDto
    {
        public required string RecipientId { get; set; }
        public DateTime? LastTimeStamp { get; set; }
    }
}
