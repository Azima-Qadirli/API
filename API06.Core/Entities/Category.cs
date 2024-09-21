using API06.Core.Entities.BaseModel;
using API06.Core.Entities.BaseModel;

namespace API06.App.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public List<Product>Products { get; set; }
}