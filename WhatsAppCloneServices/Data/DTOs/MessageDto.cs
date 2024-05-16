namespace WhatsAppCloneServices.Data.DTOs
{
    public class MessageDto
    {
        public required string RecipientUserId { get; set; }
        public required string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
