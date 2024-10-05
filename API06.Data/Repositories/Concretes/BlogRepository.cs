using API06.App.Entities;
using API06.Core.Repositories.Abstractions;
using API06.Data.Context;

namespace API06.App.Repositories.Concretes;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    public BlogRepository(AppDbContext context) : base(context)
    {
    }
}