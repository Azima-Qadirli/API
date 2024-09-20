using System.Linq.Expressions;
using API06.App.Context;
using API06.App.Entities.BaseModel;
using API06.App.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace API06.App.Repositories.Concretes;

public class Repository<T>:IRepository<T>where T:BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().AsQueryable();
    }

    public  async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().FirstOrDefaultAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync();
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }
}