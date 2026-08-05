namespace SignalR_Chat.Backend.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public bool IsGroup { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<ConversationMember> Members { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
    }

    public class ConversationMember
    {
        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; } = null!;
        
        public int UserId { get; set; }

        public User User { get; set; } = null!;
        
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}