using System.Collections.Concurrent;

namespace SignalR_Chat.Backend.Services
{
    public class OnlineUserService
    {
        private readonly ConcurrentDictionary<string, OnlineUser> _users = new();
        
        public IEnumerable<OnlineUser> Users => _users.Values;

        public void AddUser(string connectionId, string userId, string username)
        {
            _users[connectionId] = new OnlineUser
            {
                ConnectionId = connectionId,
                UserId = userId,
                Username = username
            };
        }
        
        public void RemoveUser(string connectionId)
        {
            _users.TryRemove(connectionId, out _);
        }
    }

    public class OnlineUser
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}