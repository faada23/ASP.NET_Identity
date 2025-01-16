using Microsoft.EntityFrameworkCore;
using IdentityPersistance.Repository.IRepository;
using System.Linq.Expressions;
using IdentityPersistance.Models;

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
        return dbSet.Where(filter).FirstOrDefault();
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
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