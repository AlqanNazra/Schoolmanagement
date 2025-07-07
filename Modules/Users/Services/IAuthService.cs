namespace SchoolManagementSystem.Modules.Users.Services
{
    using SchoolManagementSystem.Modules.Users.Dtos;
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        // Task<AuthResponse> RefreshTokenAsync(string token);
        // Task LogoutAsync(string token);
        // Task<bool> ValidateTokenAsync(string token);
    }
}