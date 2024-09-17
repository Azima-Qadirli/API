using API06.App.DTOs.Category;
using API06.App.Entities;
using AutoMapper;

namespace API06.App.Mapping;

public class CategoryMap:Profile
{
    public CategoryMap()
    {
        CreateMap<CategoryPostDto, Category>().ReverseMap();
        CreateMap<Category, CategoryGetDTO>().ReverseMap();
    }
}