using API06.Service.ApiResponses;
using API06.Service.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace API06.Service.Services.AbstractServices;

public interface ICategoryServices
{
    public  Task<ApiResponse> Create([FromBody] CategoryPostDto dto);
    public  Task<ApiResponse> GetAll();
    public  Task<ApiResponse> GetById(Guid id);
    public Task<ApiResponse> Remove(Guid id);
    public  Task<ApiResponse> Update(Guid id, CategoryPutDto dto);
}