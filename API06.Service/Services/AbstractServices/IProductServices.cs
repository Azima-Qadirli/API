using API06.Service.ApiResponses;
using API06.Service.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace API06.Service.Services.AbstractServices;

public interface IProductServices
{
    public  Task<ApiResponse> Create([FromBody] ProductPostDto dto);
    public  Task<ApiResponse> GetAll();
    public  Task<ApiResponse> GetById(Guid id);
    public Task<ApiResponse> Remove(Guid id);
    public  Task<ApiResponse> Update(Guid id, ProductPutDto dto);
}