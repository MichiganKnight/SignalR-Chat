using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR_Chat.Backend.Services;

namespace SignalR_Chat.Backend.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly OnlineUserService _onlineUsers;
        
        public ChatHub(OnlineUserService onlineUsers)
        {
            _onlineUsers = onlineUsers;
        }
        
        public override async Task OnConnectedAsync()
        {
            string userId = Context.User?.FindFirst("sub")?.Value ?? "";
            string username = Context.User?.Identity?.Name ?? "Unknown";
            
            _onlineUsers.AddUser(Context.ConnectionId, userId, username);

            await Clients.All.SendAsync("UserConnected", username);
            await Clients.All.SendAsync("OnlineUsersUpdated", _onlineUsers.Users);
            
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _onlineUsers.RemoveUser(Context.ConnectionId);
            
            await Clients.All.SendAsync("OnlineUsersUpdated", _onlineUsers.Users);
            
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            string username = Context.User?.Identity?.Name ?? "Unknown";
            
            await Clients.All.SendAsync("ReceiveMessage", username, message, DateTime.UtcNow);
        }
    }
}