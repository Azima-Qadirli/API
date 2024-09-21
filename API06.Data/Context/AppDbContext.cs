using API06.App.Entities;
using API06.Core.Entities.BaseModel;
using Microsoft.EntityFrameworkCore;

namespace API06.Data.Context;

public class AppDbContext:DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
}