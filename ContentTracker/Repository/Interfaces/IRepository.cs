using System.Linq.Expressions;
using ContentTracker.Entities;

namespace ContentTracker.Repository;

public interface IRepository<TEntity>
    where TEntity : IContentTrackerEntity
{
    Task<TEntity> Create(TEntity e);
    Task<TEntity?> Read(Guid id);
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
