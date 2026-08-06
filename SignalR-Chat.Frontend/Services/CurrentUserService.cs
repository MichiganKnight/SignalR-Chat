using SignalR_Chat.Frontend.Models;

namespace SignalR_Chat.Frontend.Services
{
    public class CurrentUserService
    {
        private CurrentUser? _currentUser;
        
        public CurrentUser? User => _currentUser;

        public void SetUser(int id, string username, string token)
        {
            _currentUser = new CurrentUser
            {
                Id = id,
                Username = username,
                Token = token
            };
        }

        public void Logout()
        {
            _currentUser = null;
        }
    }
}