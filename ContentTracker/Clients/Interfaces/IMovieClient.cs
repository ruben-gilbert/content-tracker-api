using ContentTracker.Entities;

namespace ContentTracker.Clients;

public interface IMovieClient
{
    public string SourceName { get; }
    public int RenewalDelay { get; }
    public Task<SourceMovieEntity> GetMovie<TIdentifier>(TIdentifier id);
    public Task<byte[]> GetBackdrop(string url);
    public Task<byte[]> GetPoster(string url);
}
