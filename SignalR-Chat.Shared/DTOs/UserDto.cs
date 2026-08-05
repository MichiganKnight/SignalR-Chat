namespace SignalR_Chat.Shared.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = string.Empty;
        
        public bool IsOnline { get; set; }
        
        public string? ProfileImage { get; set; }
    }
}