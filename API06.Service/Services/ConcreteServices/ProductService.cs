using API06.Core.Entities.BaseModel;
using API06.Core.Repositories.Abstractions;
using API06.Service.ApiResponses;
using API06.Service.DTOs.Product;
using API06.Service.Extensions;
using API06.Service.Services.AbstractServices;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace API06.App.Services.ConcreteServices;

public class ProductService:IProductServices
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    public ProductService(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _env = env;
    }

    public async Task<ApiResponse> Create(ProductPostDto dto)
    {
        Product product = _mapper.Map<Product>(dto);
        product.Image = await dto.File.SaveFileAsync(_env.WebRootPath,"assets/images/products/");
        product.CategoryId= dto.CategoryId;
        await _productRepository.AddAsync(product);
        await _productRepository.SaveAsync();
        return new ApiResponse{StatusCode = 201,Message = "Product Created Successfully",Data=""};
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

    public Task<ApiResponse> Update(Guid id, ProductPutDto dto)
    {
        throw new NotImplementedException();
    }
}

