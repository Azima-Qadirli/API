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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API06.App.Client;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    // private readonly ICategoryRepository _categoryRepository;
    // private readonly IMapper _mapper;
    private readonly ICategoryServices _categoryServices;

    public CategoriesController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }



[HttpGet("category/getall")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _categoryServices.GetAll();
        return StatusCode(res.StatusCode,res.Data);
    }
[HttpGet("category/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
     var res = await _categoryServices.GetById(id);
     return StatusCode(res.StatusCode, res.Data);
    }


    
}