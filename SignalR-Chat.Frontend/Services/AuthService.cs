using SignalR_Chat.Shared.DTOs;

namespace SignalR_Chat.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDto?> Register(RegisterDto request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            
            return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }

        public async Task<AuthResponseDto?> Login(LoginDto request)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            
            return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }
    }
}