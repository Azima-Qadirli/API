using API06.Service.ApiResponses;
using API06.Service.DTOs.Blog;
using API06.Service.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace API06.Service.Services.AbstractServices;

public interface IBlogService
{
    public  Task<ApiResponse> Create([FromBody] BlogPostDto dto);
    public  Task<ApiResponse> GetAll();
    public  Task<ApiResponse> GetById(Guid id);
    public Task<ApiResponse> Remove(Guid id);
    public  Task<ApiResponse> Update(Guid id, BlogPutDto dto);
}