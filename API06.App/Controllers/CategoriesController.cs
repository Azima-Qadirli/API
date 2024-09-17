using System.Net;
using API06.App.Context;
using API06.App.DTOs.Category;
using API06.App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API06.App.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
private readonly AppDbContext _context;

public CategoriesController(AppDbContext context)
{
    _context = context;
}
[HttpPost]
public async Task<IActionResult> Create([FromBody]CategoryPostDto dto)
{
        Category category = Map(dto);   
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return StatusCode(201);
    
    }
[HttpGet("category/getall")]
    public async Task<IActionResult> GetAll()
    {
        //var categories= await _context.Categories.ToListAsync();
       var categories = _context.Categories.AsQueryable();
       List<CategoryGetDTO>dtos = new List<CategoryGetDTO>();
       dtos = await categories.Select(c=>new CategoryGetDTO{Name=c.Name,Id=c.Id}).ToListAsync();
        return Ok(dtos);
    }
[HttpGet("category/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
     var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
     if (category == null)
     {
         return StatusCode(404,new { message =$"{category} is not found" });
     }
     return StatusCode(200, category);
     // var category = await _context.Categories.FindAsync(id);
    // var catgory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
[HttpDelete("category/{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
        if (category == null)
            return StatusCode(404,new { message =$"{category} is not found" });
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return StatusCode(204);
        
    }
[HttpPut("category/{id}")]
    public async Task<IActionResult> Update(Guid id, Category category)
    {
        var updatedCategory = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
        if (updatedCategory == null)
            return StatusCode(404,new { message =$"{category} is not found" });
        updatedCategory.Name = category.Name;
        await _context.SaveChangesAsync();
        return StatusCode(204);
    }

    private Category Map(CategoryPostDto dto)
    {
        Category category = new Category()
        {
            Name = dto.Name
        };
        return category;
    }
}