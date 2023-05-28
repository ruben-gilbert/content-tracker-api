using ContentTracker.Entities;

namespace ContentTracker.Models;

public class SourceMovie : Source
{
    public string? ImdbId { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? Runtime { get; set; }
    public string? Status { get; set; }
    public string? Title { get; set; }

    public static SourceMovie From(SourceMovieEntity e)
    {
        return new SourceMovie
        {
            SourceId = e.SourceId,
            SourceName = e.SourceName,
            ImdbId = e.ImdbId,
            ReleaseDate = e.ReleaseDate,
            Runtime = e.Runtime,
            Status = e.Status,
            Title = e.Title
        };
    }
}
