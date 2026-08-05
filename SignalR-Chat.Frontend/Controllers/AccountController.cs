using Microsoft.AspNetCore.Mvc;

namespace SignalR_Chat.Frontend.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
    }
}