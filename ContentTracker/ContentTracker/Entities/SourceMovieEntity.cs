using TmdbMovie = TMDbLib.Objects.Movies.Movie;

namespace ContentTracker.Entities;

public class SourceMovieEntity : SourceEntity, ISourceEntity
{
    public string? BackdropUri { get; set; }
    public string? ImdbId { get; set; }
    public string? PosterUri { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? Runtime { get; set; }
    public string? Status { get; set; }
    public string? Title { get; set; }
    public Guid MovieId { get; set; }

    public static SourceMovieEntity From(TmdbMovie t)
    {
        return new SourceMovieEntity
        {
            BackdropUri = t.BackdropPath,
            ImdbId = t.ImdbId,
            PosterUri = t.PosterPath,
            ReleaseDate = t.ReleaseDate,
            Runtime = t.Runtime,
            SourceId = t.Id,
            SourceName = "tmdb",
            Status = t.Status,
            Title = t.Title,
            LastRenewed = DateTime.UtcNow
        };
    }
}
