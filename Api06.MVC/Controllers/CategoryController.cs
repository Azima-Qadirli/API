using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace Api06.MVC.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryServices _categoryService;

    public CategoryController(ICategoryServices categoryService)
    {
        _categoryService = categoryService;
    }

    public async  Task<IActionResult> Index()
    {
         var data = await _categoryService.GetAll();
         if (data.StatusCode != 200)
         {
             return NotFound();
         }
         return View(data.Data);
    }
}