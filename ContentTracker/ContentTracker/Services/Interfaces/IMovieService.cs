using ContentTracker.Entities;

namespace ContentTracker.Services;

public interface IMovieService<T>
    where T : IMovieEntity
{
    public Task<T> AddFromSource(string sourceName, int sourceId);
    public Task<T> GetById(Guid id);
    public Task<SourceMovieEntity> RenewFromSource(Guid id, string sourceName);
}
