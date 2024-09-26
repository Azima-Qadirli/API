using API06.Core.Entities.BaseModel;
using API06.Service.DTOs.Product;
using AutoMapper;

namespace API06.App.Mapping;

public class ProductMap:Profile
{
    public ProductMap()
    {
        CreateMap<ProductPostDto, Product>().ReverseMap();
        CreateMap<ProductPutDto, Product>().ReverseMap();
        CreateMap<Product,ProductGetDto>().ReverseMap();

    }
}