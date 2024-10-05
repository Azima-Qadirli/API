using API06.Service.ApiResponses;
using API06.Service.DTOs.Auths;
using Microsoft.AspNetCore.Mvc;

namespace API06.Service.Services.AbstractServices;

public interface IAuthService
{
    public  Task<ApiResponse> Login([FromForm] LoginDto dto);
    public Task<ApiResponse> Register([FromForm] RegisterDto dto);
}