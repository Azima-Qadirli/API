using API06.Core.Entities.BaseModel;
using API06.Core.Repositories.Abstractions;
using API06.Data.Context;

namespace API06.App.Repositories.Concretes;

public class ProductRepository:Repository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}