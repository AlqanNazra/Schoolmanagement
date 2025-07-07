namespace SchoolManagementSystem.Modules.Users.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Mvc;
    using SchoolManagementSystem.Modules.Users.Dtos;
    using SchoolManagementSystem.Modules.Users.Services;
    using System.Threading.Tasks;
    using SchoolManagementSystem.Configurations.AppDbContext;
    using SchoolManagementSystem.Modules.Users.Entities;

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _db.Users.AnyAsync(u => u.UserName == request.UserName))
            {
                throw new Exception("User Sudah Digunakan");
            }

            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return GenerateJwt(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Username atau Password Salah");
            }

            return GenerateJwt(user);
        }

        private AuthResponse GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName,
                Role = user.Role,
                ExpiresAt = expires
            };
        }
    }
}

