using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Backend.Entities;
using SignalR_Chat.Shared.DTOs;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SignalR_Chat.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Email Already Exists"
                });
            }

            if (await _userManager.FindByNameAsync(request.Username) != null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Username Already Exists"
                });
            }

            ApplicationUser user = new()
            {
                UserName = request.Username,
                Email = request.Email
            };
            
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))
                });
            }
            
            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "Account Created Successfully",
                UserId = user.Id,
                Username = user.UserName
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid Email or Password"
                });
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid Email or Password"
                });
            }
            
            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "Login Successful",
                UserId = user.Id,
                Username = user.UserName
            });
        }
    }
}