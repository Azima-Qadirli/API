using API06.App.Entities;
using API06.Service.DTOs.Category;
using API06.Core.Entities;
using AutoMapper;

namespace API06.App.Mapping;

public class CategoryMap:Profile
{
    public CategoryMap()
    {
        CreateMap<CategoryPostDto, Category>().ReverseMap();
        CreateMap<CategoryPutDto, Category>().ReverseMap();
        CreateMap<Category, CategoryGetDTO>().ReverseMap();
        
    }
}