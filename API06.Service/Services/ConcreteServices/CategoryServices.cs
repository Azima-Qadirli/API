using API06.Service.ApiResponses;
using API06.Service.DTOs.Category;
using API06.App.Entities;
using API06.Core.Repositories.Abstractions;
using API06.Service.Services.AbstractServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API06.App.Services.ConcreteServices;

public class CategoryServices:ICategoryServices
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Create(CategoryPostDto dto)
    {
        Category category =_mapper.Map<Category>(dto);   
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();
        return new  ApiResponse{StatusCode = 201};
    }

    public async  Task<ApiResponse> GetAll()
    {
        var categories=  _categoryRepository.GetAll(x=>!x.IsDeleted);
        List<CategoryGetDTO>dtos = new List<CategoryGetDTO>();
        dtos = await categories.Select(c=>new CategoryGetDTO{Name=c.Name,Id=c.Id}).ToListAsync();
         return new ApiResponse{StatusCode = 200,Data=dtos};
    }

    public async  Task<ApiResponse> GetById(Guid id)
    {
        var category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
        if (category == null)
        {
            return new ApiResponse{StatusCode = 404,Message = "Item is not found"};
        }
        CategoryGetDTO dto = _mapper.Map<CategoryGetDTO>(category);
        return new ApiResponse{StatusCode = 200,Data=dto};
    }

    public async Task<ApiResponse> Remove(Guid id)
    {
        var category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
        if (category == null)
            return new ApiResponse{StatusCode = 404,Message = "Item is not found"};
        category.IsDeleted = true;
        await _categoryRepository.SaveAsync();
        return new ApiResponse{StatusCode = 204};
    }

    public async Task<ApiResponse> Update(Guid id, CategoryPutDto dto)
    {
        var category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
        if (category == null)
            return new ApiResponse{StatusCode = 404,Message = "Item is not found"};
        category.Name = dto.Name;
        category.UpdatedAt=DateTime.Now;
        await _categoryRepository.SaveAsync();
        return new ApiResponse{StatusCode = 204,Message="Item is updated"};
    }
}