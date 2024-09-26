using System.ComponentModel.DataAnnotations.Schema;
using API06.App.Entities;

namespace API06.Core.Entities.BaseModel;

public class Product:BaseEntity
{
    public string Name {  get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image {  get; set; }

    //relations
    [ForeignKey(nameof(Category))]
    public Guid CategoryId {  get; set; }
    public Category Category { get; set; }
    
}