namespace SignalR_Chat.Backend.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastSeen { get; set; }

        public ICollection<ConversationMember> Conversations { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
    }
}