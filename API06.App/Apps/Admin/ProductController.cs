using API06.Service.DTOs.Product;
using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API06.App.Admin;
[Authorize(Roles="Admin,SuperAdmin")]
[ApiController]

[Route("/api/admin/[controller]")]

public class ProductController : ControllerBase
{
    
   private readonly IProductServices _productServices;

   public ProductController(IProductServices productServices)
   {
      _productServices = productServices;
   }
   [HttpDelete]
   public async Task<IActionResult> Delete(Guid id)
   {
      var res = await _productServices.Remove(id);
      return Ok(res.StatusCode);
   }
   
   
   [HttpPost]

   public async Task<IActionResult> Create([FromForm ]ProductPostDto dto)
   {
      var res = await _productServices.Create(dto);
      return StatusCode(res.StatusCode, res.Message);
   }

   [HttpPut("{id}")]
   public async  Task<IActionResult> Update(Guid id, [FromForm] ProductPutDto dto)
   {
      var res = await  _productServices.Update(id, dto);
      return Ok(res);
   }
}