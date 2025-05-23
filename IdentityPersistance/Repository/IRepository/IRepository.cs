using System.Linq.Expressions;

namespace IdentityPersistance.Repository.IRepository;

public interface IRepository<T> where T : class
{
    T Get(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetAll(string? includeProperties = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    void Update(T entity);

    void Save();
}