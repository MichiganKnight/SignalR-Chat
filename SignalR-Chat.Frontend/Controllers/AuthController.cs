using Microsoft.AspNetCore.Mvc;

namespace SignalR_Chat.Frontend.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Restore()
        {
            return Json(new
            {
                success = true
            });
        }
    }
}