namespace SignalR_Chat.Shared.DTOs
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        
        public string Message { get; set; } = string.Empty;
        
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public string? Token { get; set; }
    }
}