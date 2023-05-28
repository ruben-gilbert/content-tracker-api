using System.Linq.Expressions;
using ContentTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentTracker.Repository;

public class SourceRepository<TEntity> : ISourceRepository<TEntity>
    where TEntity : SourceEntity
{
    private RepositoryContext _context;

    public SourceRepository(RepositoryContext context)
    {
        _context = context;
    }

    private IQueryable<TEntity> ProcessQuery(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable<TEntity>();
        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query;
    }

    public async Task<TEntity> Create(TEntity e)
    {
        await _context.Set<TEntity>().AddAsync(e);
        await _context.SaveChangesAsync();
        return e;
    }

    public async Task<TEntity?> Read(string name, int id)
    {
        return await _context.Set<TEntity>().FindAsync(name, id);
    }

    public async Task<TEntity?> Read(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = ProcessQuery(filter, orderBy, includes);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> ReadMany(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes
    )
    {
        IQueryable<TEntity> query = ProcessQuery(filter, orderBy, includes);
        return await query.ToListAsync();
    }

    public async Task<TEntity?> Update(TEntity updated)
    {
        TEntity? orig = await Read(updated.SourceName, updated.SourceId);
        if (orig == null)
        {
            return null;
        }

        _context.Entry(orig).CurrentValues.SetValues(updated);
        _context.Set<TEntity>().Update(orig);
        await _context.SaveChangesAsync();
        return orig;
    }

    public async Task Delete(TEntity e)
    {
        _context.Set<TEntity>().Remove(e);
        await _context.SaveChangesAsync();
    }
}
