namespace SignalR_Chat.Shared.DTOs
{
    public class ConversationDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public bool IsGroup { get; set; }

        public List<UserDto> Members { get; set; } = [];
        public List<MessageDto> Messages { get; set; } = [];
    }
}