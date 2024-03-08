using System.Linq.Expressions;

namespace TeduBlog.Core.SeedWorks;

public interface IRepositoryBase<T, in TK> where T : class
{
    Task<T?> GetByIdAsync(TK id);

    Task<IEnumerable<T>> GetAllAsync();

    IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}