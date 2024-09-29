using API06.Service.DTOs.Auths;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API06.App.Apps.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthsController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

    }
}
