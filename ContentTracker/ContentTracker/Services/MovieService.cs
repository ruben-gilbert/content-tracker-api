using ContentTracker.Clients;
using ContentTracker.Entities;
using ContentTracker.Exceptions;
using ContentTracker.Repository;

namespace ContentTracker.Services;

public class MovieService : IMovieService<MovieEntity>
{
    private readonly IEnumerable<IMovieClient> _clients;
    private readonly IRepository<MovieEntity> _repository;
    private readonly ISourceRepository<SourceMovieEntity> _sourceRepository;

    public MovieService(
        IEnumerable<IMovieClient> clients,
        IRepository<MovieEntity> repository,
        ISourceRepository<SourceMovieEntity> sourceRepository
    )
    {
        _clients = clients;
        _repository = repository;
        _sourceRepository = sourceRepository;
    }

    private IMovieClient GetSourceClient(string sourceName)
    {
        IMovieClient? c = _clients.FirstOrDefault(c => c.SourceName == sourceName);
        if (c == null)
        {
            throw new InvalidSourceException($"No known source '{sourceName}'");
        }

        return c;
    }

    public async Task<MovieEntity> AddFromSource(string sourceName, int sourceId)
    {
        IMovieClient client = GetSourceClient(sourceName);
        SourceMovieEntity? existing = await _sourceRepository.Read(sourceName, sourceId);
        if (existing != null)
        {
            throw new SourceContentCachedException($"Source: {sourceName}.  ID: {sourceId}");
        }

        SourceMovieEntity s = await client.GetMovie(sourceId);
        return await _repository.Create(MovieEntity.From(s));
    }

    public async Task<MovieEntity> GetById(Guid id)
    {
        MovieEntity? e = await _repository.Read(filter: m => m.Id == id, includes: m => m.Sources);
        if (e == null)
        {
            throw new TrackedContentNotFoundException(
                $"Movie with Tracker ID '{id}' does not exist"
            );
        }

        return e;
    }

    public async Task<SourceMovieEntity> RenewFromSource(Guid id, string sourceName)
    {
        MovieEntity? m = await _repository.Read(filter: m => m.Id == id, includes: m => m.Sources);
        if (m == null)
        {
            throw new TrackedContentNotFoundException($"ID: {id}");
        }

        IMovieClient client = GetSourceClient(sourceName);
        SourceMovieEntity? s = m.Sources.FirstOrDefault(e => e.SourceName == sourceName);
        if (s == null)
        {
            throw new SourceContentNotCachedException($"Source: {sourceName}");
        }

        int secondsSinceRenewal = (int)DateTime.UtcNow.Subtract(s.LastRenewed).TotalSeconds;
        if (secondsSinceRenewal < client.RenewalDelay)
        {
            throw new SourceRenewedTooSoonException(
                client.RenewalDelay - secondsSinceRenewal,
                $"Source: {sourceName}.  ID: {s.SourceId}.  Delay: {client.RenewalDelay}.  Since last renewal: {secondsSinceRenewal}"
            );
        }

        SourceMovieEntity updated = await client.GetMovie(s.SourceId);
        SourceMovieEntity? renewed = await _sourceRepository.Update(updated);
        if (renewed == null)
        {
            throw new CachedContentNotFoundException($"Source: {sourceName}.  ID: {s.SourceId}");
        }

        return renewed;
    }
}
