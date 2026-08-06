using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Chat.Backend.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            string username = Context.User?.Identity?.Name ?? "Unknown";

            await Clients.All.SendAsync("UserConnected", username);
            
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Context.User?.Identity?.Name ?? "Unknown";
            
            await Clients.All.SendAsync("UserDisconnected", username);
            
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            string username = Context.User?.Identity?.Name ?? "Unknown";
            
            await Clients.All.SendAsync("ReceiveMessage", username, message, DateTime.UtcNow);
        }
    }
}