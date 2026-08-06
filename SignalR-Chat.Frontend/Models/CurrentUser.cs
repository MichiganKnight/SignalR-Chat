namespace SignalR_Chat.Frontend.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        
        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
    }
}