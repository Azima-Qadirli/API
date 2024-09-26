using API06.Service.DTOs.Product;
using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Mvc;
namespace API06.App.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    
   private readonly IProductServices _productServices;

   public ProductController(IProductServices productServices)
   {
      _productServices = productServices;
   }

   [HttpPost]

   public async Task<IActionResult> Create([FromForm ]ProductPostDto dto)
   {
      var res = await _productServices.Create(dto);
      return StatusCode(res.StatusCode, res.Message);
   }
}