using System.Linq.Expressions;
using System.Net;
using API06.Data.Context;
using API06.Service.DTOs.Category;
using API06.App.Entities;
using API06.App.Repositories;
using API06.Service.Services.AbstractServices;
using API06.Core.Repositories.Abstractions;
using API06.Service.DTOs.Category;
using API06.Service.Services.AbstractServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API06.App.Admin;
[Authorize(Roles="Admin,SuperAdmin")]
[ApiController]
[Route("api/admin/[controller]")]


public class CategoriesController : ControllerBase
{
    // private readonly ICategoryRepository _categoryRepository;
    // private readonly IMapper _mapper;
    private readonly ICategoryServices _categoryServices;

    public CategoriesController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }


    [HttpPost]
public async Task<IActionResult> Create([FromBody]CategoryPostDto dto)
{
   var res = await _categoryServices.Create(dto);
    return StatusCode(res.StatusCode);
}
[HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var res = await _categoryServices.Remove(id);
        return StatusCode(res.StatusCode);
        
    }
[HttpPut("category/{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryPutDto dto)
    {
        var res = await _categoryServices.Update(id,dto);
        return StatusCode(res.StatusCode,res.Message);
    }

    
}