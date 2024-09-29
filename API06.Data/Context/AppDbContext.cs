using API06.App.Entities;
using API06.Core.Entities.BaseModel;
using API06.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API06.Data.Context;

public class AppDbContext:IdentityDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    //     modelBuilder.ApplyConfiguration(new ProductConfiguration());
    //     base.OnModelCreating(modelBuilder);
    // }
}