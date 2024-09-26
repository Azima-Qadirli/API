using API06.Data.Context;
using API06.App.Entities;
using API06.Core.Repositories.Abstractions;

namespace API06.App.Repositories.Concretes;

public class CategoryRepository:Repository<Category>,ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
        
    }
}