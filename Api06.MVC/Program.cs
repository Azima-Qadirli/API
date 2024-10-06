using System.Text;
using API06.App.Mapping;
using API06.App.Repositories.Concretes;
using API06.App.Services.ConcreteServices;
using API06.App.Validations.Category;
using API06.Core.Repositories.Abstractions;
using API06.Data.Context;
using API06.Service.Services.AbstractServices;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();

//services
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddAutoMapper(typeof(CategoryMap));
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDTOValidation>());
//builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("api06", option =>
    {
        option.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("api06");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();