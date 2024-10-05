using Microsoft.AspNetCore.Http;

namespace API06.Service.DTOs.Blog;

public class BlogPostDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }
    
    //relations
    public Guid CategoryId { get; set; }
}