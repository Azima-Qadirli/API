using Microsoft.AspNetCore.Http;

namespace API06.Service.DTOs.Product;

public record ProductPutDto
{
    public string Name {  get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string CategoryId {  get; set; }
    public IFormFile File { get; set; }
}