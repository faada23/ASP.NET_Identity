using System.Linq.Expressions;

namespace IdentityPersistance.Repository.IRepository;

public interface IRepository<T> where T : class
{
    T Get(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    void Update(T entity);

    void Save();
}