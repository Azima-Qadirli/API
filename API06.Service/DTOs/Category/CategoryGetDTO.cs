namespace API06.Service.DTOs.Category;

public record CategoryGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}