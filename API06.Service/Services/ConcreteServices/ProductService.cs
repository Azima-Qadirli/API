using API06.Core.Entities.BaseModel;
using API06.Core.Repositories.Abstractions;
using API06.Service.ApiResponses;
using API06.Service.DTOs.Product;
using API06.Service.Extensions;
using API06.Service.Services.AbstractServices;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API06.App.Services.ConcreteServices;

public class ProductService:IProductServices
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse> Create(ProductPostDto dto)
    {
        Product product = _mapper.Map<Product>(dto);
        product.Image = await dto.File.SaveFileAsync(_env.WebRootPath,"assets/img/product/");
        var req = _httpContextAccessor.HttpContext.Request;
        product.ImageUrl =req.Scheme+"://"+req.Host+"/assets/img/product/";
        product.CategoryId= dto.CategoryId;
        await _productRepository.AddAsync(product);
        await _productRepository.SaveAsync();
        return new ApiResponse{StatusCode = 201,Message = "Product Created Successfully",Data=""};
    }

    public async Task<ApiResponse> GetAll()
    {
        var categories =  _productRepository.GetAll(c=>!c.IsDeleted);
        List<ProductGetDto> dtos = categories.Select(c => new ProductGetDto
        {
            Id = c.Id,Name = c.Name, Price = c.Price, Image=c.Image,Description = c.Description, Category = c.Category
        }).ToList();
        return new ApiResponse { StatusCode = 200, Data = dtos, Message = "Here are items" };
    }

    public async  Task<ApiResponse> GetById(Guid id)
    {
        var product = await _productRepository.GetAsync(c=>!c.IsDeleted);
        if(product==null)
            return new ApiResponse { StatusCode = 404, Message = "Item is not found" }; 
        ProductGetDto productGetDto = _mapper.Map<ProductGetDto>(product);
        
        return new ApiResponse { StatusCode = 200, Data = productGetDto,Message = "Success"};
        
    }

    public async Task<ApiResponse> Remove(Guid id)
    {
        var product = await _productRepository.GetAsync(c=>!c.IsDeleted);
        if(product==null)
            return new ApiResponse { StatusCode = 404, Message = "Item is not found" }; 
        product.IsDeleted = true;
        await _productRepository.SaveAsync();
        return new ApiResponse { StatusCode = 204};
    }

    public async Task<ApiResponse> Update(Guid id, ProductPutDto dto)
    {
        var product = await _productRepository.GetAsync(c=>!c.IsDeleted);
        if(product==null)
            return new ApiResponse { StatusCode = 404, Message = "Item is not found" }; 
        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Description=dto.Description;
        product.CategoryId = dto.CategoryId;
        product.Image = dto.File == null?product.Image: await dto.File.SaveFileAsync(_env.WebRootPath,"assets/img/product/");
        product.UpdatedAt = DateTime.UtcNow;
        _productRepository.Update(product);
        await _productRepository.SaveAsync();
        return new ApiResponse { StatusCode = 200, Data = dto};
    }
}

