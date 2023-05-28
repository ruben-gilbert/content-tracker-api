using System.ComponentModel.DataAnnotations;

namespace ContentTracker.Entities;

public class MovieEntity : ContentTrackerEntity, IMovieEntity
{
    [Required]
    public virtual IList<SourceMovieEntity> Sources { get; set; } = new List<SourceMovieEntity>();

    public static MovieEntity From(SourceMovieEntity s)
    {
        return new MovieEntity
        {
            Id = Guid.NewGuid(),
            Sources = new List<SourceMovieEntity>() { s }
        };
    }
}
