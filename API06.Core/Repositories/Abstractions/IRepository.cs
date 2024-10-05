using System.Linq.Expressions;
using API06.Core.Entities.BaseModel;

namespace API06.Core.Repositories.Abstractions;

public interface IRepository<T> where T:BaseEntity
{
    public Task AddAsync(T entity);
    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression );
    public Task<T> GetAsync(Expression<Func<T, bool>> expression);
    public Task<T> GetByIdAsync(Guid id);
    public void Update(T entity);
    public void Delete(T entity);
    public Task<int> SaveAsync();
    public int Save();
}