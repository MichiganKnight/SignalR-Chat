using Microsoft.AspNetCore.Identity;

namespace SignalR_Chat.Backend.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastSeen { get; set; }
        
        public ICollection<ConversationMember> Conversations { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
    }
}