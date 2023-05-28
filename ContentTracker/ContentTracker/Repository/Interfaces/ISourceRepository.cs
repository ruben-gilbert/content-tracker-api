using System.Linq.Expressions;
using ContentTracker.Entities;

namespace ContentTracker.Repository;

public interface ISourceRepository<TEntity>
    where TEntity : ISourceEntity
{
    Task<TEntity> Create(TEntity e);
    Task<TEntity?> Read(string name, int id);
    Task<TEntity?> Read(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    Task<IEnumerable<TEntity>> ReadMany(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes
    );
    Task<TEntity?> Update(TEntity updated);
    Task Delete(TEntity e);
}
