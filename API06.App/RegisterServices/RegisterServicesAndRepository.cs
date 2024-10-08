using API06.App.Repositories.Concretes;
using API06.App.Services.ConcreteServices;
using API06.Core.Repositories.Abstractions;
using API06.Data.Context;
using API06.Service.Services.AbstractServices;
using Microsoft.EntityFrameworkCore;

namespace API06.App.RegisterServices;

public static class RegisterServicesAndRepository
{
    
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("Default"));
        });
        services.AddCors(opt =>
        {
            opt.AddPolicy("api06", option =>
            {
                option.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });
        //repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();


//services
        services.AddScoped<ICategoryServices, CategoryServices>();
        services.AddScoped<IProductServices, ProductService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBlogService, BlogService>();

        return services;
    }
}