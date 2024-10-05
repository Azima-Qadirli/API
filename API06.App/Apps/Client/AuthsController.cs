using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API06.Service.DTOs.Auths;
using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API06.App.Apps.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            var res = await _authService.Login(dto);
            return StatusCode(res.StatusCode, res.Data);
        }
        

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterDto dto)
        {
            var res =await  _authService.Register(dto);
            return StatusCode(res.StatusCode, res.Data);
        }
        // [HttpPost("CreateRole")]
//         public async Task<IActionResult> CreateRole()
//         {
//             IdentityRole admin = new (){ Name = "Admin" };
//             IdentityRole superAdmin = new (){ Name = "SuperAdmin" };
//             IdentityRole user = new (){ Name = "User" };
//
//             await _roleManager.CreateAsync(admin);
//             await _roleManager.CreateAsync(superAdmin);
//             await _roleManager.CreateAsync(user);
//             return Ok();
//         }

    }
}
