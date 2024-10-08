using System.Text;
using API06.App.Extensions;
using API06.Data.Context;
using API06.App.Mapping;
using API06.App.RegisterServices;
using API06.Core.Repositories.Abstractions;
using API06.App.Repositories.Concretes;
using API06.Service.Services.AbstractServices;
using API06.App.Services.ConcreteServices;
using API06.App.Validations.Category;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
//builder.Services.AddValidatorsFromAssemblyContaining<CategoryPostDTOValidation>().AddFluentValidationClientsideAdapters();
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDTOValidation>());
//builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());
builder.Services.AddAutoMapper(typeof(CategoryMap));

//Repositories
builder.Services
    .RegisterServices(builder.Configuration)
    .RegisterJWTServices(builder.Configuration)
    .RegisterUserServices();












var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureException();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.UseCors("api06");
app.Run();

