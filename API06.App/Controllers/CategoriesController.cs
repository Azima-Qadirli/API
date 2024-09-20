using System.Linq.Expressions;
using System.Net;
using API06.App.Context;
using API06.App.DTOs.Category;
using API06.App.Entities;
using API06.App.Repositories;
using API06.App.Repositories.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API06.App.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

public CategoriesController( IMapper mapper, ICategoryRepository categoryRepository)
{
    _mapper = mapper;
    _categoryRepository = categoryRepository;
}
[HttpPost]
public async Task<IActionResult> Create([FromBody]CategoryPostDto dto)
{
        Category category =_mapper.Map<Category>(dto);   
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();
        return StatusCode(201);
    
    }
[HttpGet("category/getall")]
    public async Task<IActionResult> GetAll()
    {
        //var categories= await _context.Categories.ToListAsync();
       var categories = _categoryRepository.GetAll(x=>!x.IsDeleted);
       List<CategoryGetDTO>dtos = new List<CategoryGetDTO>();
       dtos = await categories.Select(c=>new CategoryGetDTO{Name=c.Name,Id=c.Id}).ToListAsync();
        return Ok(dtos);
    }
[HttpGet("category/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
     var category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
     if (category == null)
     {
         return StatusCode(404,new { message =$"Item is not found" });
     }
     CategoryGetDTO dto = _mapper.Map<CategoryGetDTO>(category);
     return StatusCode(200, dto);
     // var category = await _context.Categories.FindAsync(id);
    // var catgory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
[HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var category = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
        if (category == null)
            return StatusCode(404,new { message =$"Item is not found" });
        category.IsDeleted = true;
        await _categoryRepository.SaveAsync();
        return StatusCode(204);
        
    }
[HttpPut("category/{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryPutDto dto)
    {
        var updatedCategory = await _categoryRepository.GetAsync(x=>!x.IsDeleted && x.Id == id);
        if (updatedCategory == null)
            return StatusCode(404,new { message =$"Item is not found" });
        updatedCategory.Name = dto.Name;
        await _categoryRepository.SaveAsync();
        return StatusCode(204);
    }

    
}