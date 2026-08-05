using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Frontend.Services;
using SignalR_Chat.Frontend.ViewModels;
using SignalR_Chat.Shared.DTOs;

namespace SignalR_Chat.Frontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        
        public AccountController(AuthService authService)
        {
            _authService = authService;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AuthResponseDto? result = await _authService.Register(new RegisterDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            });
            
            if (result?.Success == true)
            {
                return RedirectToAction("Login");
            }
            
            ModelState.AddModelError(string.Empty, result?.Message ?? "Registration Failed");
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AuthResponseDto? result = await _authService.Login(new LoginDto
            {
                Email = model.Email,
                Password = model.Password
            });

            if (result?.Success == true)
            {
                return RedirectToAction("Index", "Chat");
            }
            
            ModelState.AddModelError(string.Empty, result?.Message ?? "Login Failed");
            
            return View(model);
        }
    }
}