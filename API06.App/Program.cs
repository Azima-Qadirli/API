using System.Text;
using API06.Data.Context;
using API06.App.Mapping;
using API06.Core.Repositories.Abstractions;
using API06.App.Repositories.Concretes;
using API06.Service.Services.AbstractServices;
using API06.App.Services.ConcreteServices;
using API06.App.Validations.Category;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
//builder.Services.AddValidatorsFromAssemblyContaining<CategoryPostDTOValidation>().AddFluentValidationClientsideAdapters();
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDTOValidation>());
//builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());
builder.Services.AddAutoMapper(typeof(CategoryMap));
//repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();


//services
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlogService, BlogService>();



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:audience"],
        ValidIssuer = builder.Configuration["JWT:issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secret_key"]))
    };
});

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.UseCors("api06");
app.Run();

