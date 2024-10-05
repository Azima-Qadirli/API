using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API06.Service.ApiResponses;
using API06.Service.DTOs.Auths;
using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API06.App.Services.ConcreteServices;

public class AuthService:IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ApiResponse> Login(LoginDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.Username);
        if (userExists == null)
            return new ApiResponse { StatusCode = 404, Message = "user not found." };
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
        return new ApiResponse { StatusCode = 200,Data=token };
    }

    public async Task<ApiResponse> Register(RegisterDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if(userExists != null)
            return new ApiResponse { StatusCode = 400, Message = "user is already exist." };
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
                // ModelState.AddModelError("", result.Description);
                return new ApiResponse { Message = result.Description };
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
        return new ApiResponse { StatusCode = 200};
    }
}