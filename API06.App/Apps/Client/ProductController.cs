using API06.Service.DTOs.Product;
using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Mvc;
namespace API06.App.Client;
[Route("/api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    
   private readonly IProductServices _productServices;

   public ProductController(IProductServices productServices)
   {
      _productServices = productServices;
   }

   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var res = await _productServices.GetAll();
      return StatusCode(res.StatusCode, res.Data);
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> Get(Guid id)
   {
      var res  = await _productServices.GetById(id);
      return Ok(res);
   }

  
}