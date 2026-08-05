using SignalR_Chat.Shared.DTOs;

namespace SignalR_Chat.Frontend.ViewModels
{
    public class ChatViewModel
    {
        public ConversationDto CurrentConversation { get; set; } = new();
        public List<ConversationDto> Conversations { get; set; } = [];
        public List<UserDto> OnlineUsers { get; set; } = [];
    }
}