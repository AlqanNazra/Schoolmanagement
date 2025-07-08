
namespace SchoolManagementSystem.Modules.Users
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
    using Microsoft.AspNetCore.Authorization;

    [Authorize (Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _auth;

        public UsersController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _auth.RegisterAsync(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _auth.LoginAsync(request);
            return Ok(response);
        }
    }
}