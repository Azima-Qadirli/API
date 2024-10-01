using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API06.Service.DTOs.Auths;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthsController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.Username);
            if(userExists == null)
                return StatusCode(400,new {description = "user not found"});
            var loggedId = await _userManager.CheckPasswordAsync(userExists, dto.Password);
            
            var userRoles = await _userManager.GetRolesAsync(userExists);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, dto.Username),
                new Claim(ClaimTypes.NameIdentifier, userExists.Id),
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //jwt -> json web token
            var secret_key =_configuration["JWT:secret_key"];
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));
            var jwttoken = new JwtSecurityToken
            (
                issuer:_configuration["JWT:issuer"],
                audience:_configuration["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
            );
            
            var token = new JwtSecurityTokenHandler().WriteToken(jwttoken);
            return Ok(token);
        }
        

[HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.UserName);
            if(userExists != null)
                return StatusCode(400,"User already exists");
            IdentityUser user = new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var res = await _userManager.CreateAsync(user);
            if (!res.Succeeded)
            {
                foreach (var result in res.Errors)
                {
                    ModelState.AddModelError("", result.Description);
                }
            }

            try
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return StatusCode(200);
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
