using API06.Core.Entities.BaseModel;

namespace API06.App.Entities;

public class Blog:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string ImageUrl { get; set; }
    
    //relations
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}