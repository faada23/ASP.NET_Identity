using Microsoft.EntityFrameworkCore;
using IdentityPersistance.Repository.IRepository;
using System.Linq.Expressions;

public class Repository <T> : IRepository<T> where T : class
{
    private readonly ProgramContext _db;
    internal DbSet<T> dbSet;
    
    public Repository(ProgramContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        return dbSet.Where(filter).FirstOrDefault()!;
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        return filter == null ? dbSet.ToList() : dbSet.Where(filter).ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(T entity)
    {
        dbSet.Update(entity);
    }

    
}