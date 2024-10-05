using API06.App.Entities;
using API06.Core.Repositories.Abstractions;
using API06.Service.ApiResponses;
using API06.Service.DTOs.Blog;
using API06.Service.Extensions;
using API06.Service.Services.AbstractServices;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API06.App.Services.ConcreteServices;

public class BlogService:IBlogService
{
    private readonly IBlogRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IWebHostEnvironment _env;
    public BlogService(IBlogRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
    {
        _repository = repository;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _env = env;
    }

    public async  Task<ApiResponse> Create(BlogPostDto dto)
    {
        Blog blog = _mapper.Map<Blog>(dto);
        string root = _env.WebRootPath;
        string path = "assets/img/blog";
        blog.Image = await dto.File.SaveFileAsync(root,path);
        var req = _contextAccessor.HttpContext.Request;
        blog.ImageUrl=req.Scheme + "://" + req.Host +"/assests/img/blog/"+ blog.Image;
        await _repository.AddAsync(blog);
        await _repository.SaveAsync();
        return new ApiResponse { StatusCode = 201 };

    }

    public Task<ApiResponse> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> Update(Guid id, BlogPutDto dto)
    {
        throw new NotImplementedException();
    }
}