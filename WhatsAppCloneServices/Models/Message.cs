namespace WhatsAppCloneServices.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public required string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public required string SenderId { get; set; }
        public required virtual User Sender { get; set; }
        public required string RecipientUserId { get; set; }
        public required virtual User Recipient { get; set; }
    }
}

