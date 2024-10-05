namespace API06.Service.DTOs.Blog;

public class BlogGetDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string ImageUrl { get; set; }
    
    //relations
    public App.Entities.Category Category { get; set; }
}