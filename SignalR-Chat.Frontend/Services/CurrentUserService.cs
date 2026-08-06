using System.Text.Json;
using SignalR_Chat.Frontend.Models;

namespace SignalR_Chat.Frontend.Services
{
    public class CurrentUserService
    {
        private const string UserKey = "CurrentUser";
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public CurrentUser? User
        {
            get
            {
                string? data = Session.GetString(UserKey);
                
                return data == null ? null : JsonSerializer.Deserialize<CurrentUser>(data);
            }
        }

        public void SetUser(int id, string username, string token)
        {
            CurrentUser user = new()
            {
                Id = id,
                Username = username,
                Token = token
            };
            
            Session.SetString(UserKey, JsonSerializer.Serialize(user));
        }

        public void Logout()
        {
            Session.Remove(UserKey);
        }
    }
}