using ContentTracker.Entities;
using ContentTracker.Exceptions;
using ContentTracker.Options;
using Microsoft.Extensions.Options;
using TMDbLib.Client;
using TmdbMovie = TMDbLib.Objects.Movies.Movie;

namespace ContentTracker.Clients;

public class TmdbMovieClient : IMovieClient
{
    private readonly SourceOptions _options;

    public string SourceName
    {
        get { return Sources.Tmdb; }
    }
    public int RenewalDelay
    {
        get { return _options.RenewalDelay; }
    }

    public TmdbMovieClient(IOptions<ContentTrackerOptions> contentTrackerOptions)
    {
        // FIXME: Throw some kind of configuration exception, if source section is missing?  Can this be validated earlier than constructor?
        _options = contentTrackerOptions.Value.Sources.First(opts => opts.Name == SourceName);
    }

    public async Task<SourceMovieEntity> GetMovie<TIdentifier>(TIdentifier id)
    {
        using TMDbClient client = new TMDbClient(_options.ApiKey);
        TmdbMovie? t = await client.GetMovieAsync(Convert.ToInt32(id));
        if (t == null)
        {
            throw new SourceContentNotFoundException($"Source: {SourceName}.  ID: {id}.");
        }

        return SourceMovieEntity.From(t);
    }

    public async Task<byte[]> GetBackdrop(string url)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> GetPoster(string url)
    {
        throw new NotImplementedException();
    }
}
