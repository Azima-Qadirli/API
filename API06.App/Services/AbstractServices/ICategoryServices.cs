using API06.App.ApiResponses;
using API06.App.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace API06.App.Services.AbstractServices;

public interface ICategoryServices
{
    public  Task<ApiResponse> Create([FromBody] CategoryPostDto dto);
    public  Task<ApiResponse> GetAll();
    public  Task<ApiResponse> GetById(Guid id);
    public Task<ApiResponse> Remove(Guid id);
    public  Task<ApiResponse> Update(Guid id, CategoryPutDto dto);
}