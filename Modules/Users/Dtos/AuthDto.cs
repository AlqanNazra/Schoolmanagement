namespace SchoolManagementSystem.Modules.Users.Dtos
{
    public class RegisterRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class LoginRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    
    public class AuthResponse
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}