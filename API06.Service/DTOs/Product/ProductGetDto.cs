namespace API06.Service.DTOs.Product;

public record ProductGetDto
{
    public Guid Id { get; set; }
    public string Name {  get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image {  get; set; }
    public string CategoryId {  get; set; }
    public App.Entities.Category Category { get; set; }
}