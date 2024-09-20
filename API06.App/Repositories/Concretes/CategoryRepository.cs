using API06.App.Context;
using API06.App.Entities;
using API06.App.Repositories.Abstractions;

namespace API06.App.Repositories.Concretes;

public class CategoryRepository:Repository<Category>,ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}