using TaskManagementSystem.DTOs;

namespace TaskManagementSystem.Services
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterDto registerDto);
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
