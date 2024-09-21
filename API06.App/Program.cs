using API06.Data.Context;
using API06.Service.DTOs.Category;
using API06.App.Entities;
using API06.App.Mapping;
using API06.Core.Repositories.Abstractions;
using API06.App.Repositories.Concretes;
using API06.Service.Services.AbstractServices;
using API06.App.Services.ConcreteServices;
using API06.App.Validations.Category;
using API06.Core.Repositories.Abstractions;
using API06.Data.Context;
using API06.Service.Services.AbstractServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
//builder.Services.AddValidatorsFromAssemblyContaining<CategoryPostDTOValidation>().AddFluentValidationClientsideAdapters();
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDTOValidation>());
//builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());
builder.Services.AddAutoMapper(typeof(CategoryMap));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();

