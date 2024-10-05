using API06.App.Entities;
using API06.Service.DTOs.Blog;
using AutoMapper;

namespace API06.App.Mapping;

public class BlogMap:Profile
{
    public BlogMap()
    {
        CreateMap<BlogPostDto, Blog>().ReverseMap();
        CreateMap<BlogPutDto,Blog>().ReverseMap();
        CreateMap<Blog, BlogGetDto>().ReverseMap();
    }
}