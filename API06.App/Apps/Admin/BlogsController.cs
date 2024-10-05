using API06.Service.DTOs.Blog;
using API06.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API06.App.Apps.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
[HttpPost("create")]
        public async Task<IActionResult> Create([FromForm]BlogPostDto dto)
        {
            var res = await _blogService.Create(dto);
            return StatusCode(res.StatusCode);
        }
    }
}
