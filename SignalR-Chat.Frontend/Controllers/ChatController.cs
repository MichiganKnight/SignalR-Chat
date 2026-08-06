using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Frontend.Services;
using SignalR_Chat.Frontend.ViewModels;

namespace SignalR_Chat.Frontend.Controllers
{
    public class ChatController : Controller
    {
        private readonly CurrentUserService _currentUser;
        
        public ChatController(CurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }
        
        public IActionResult Index()
        {
            if (!_currentUser.User?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "Account");
            }

            ChatViewModel model = new();
            
            return View(model);
        }
    }
}