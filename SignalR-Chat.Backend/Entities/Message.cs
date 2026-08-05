namespace SignalR_Chat.Backend.Entities
{
    public class Message
    {
        public int Id { get; set; }
        
        public string Content { get; set; } = string.Empty;
        
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        
        public int SenderId { get; set; }

        public User Sender { get; set; } = null!;
        
        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; } = null!;
    }
}