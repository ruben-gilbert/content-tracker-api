using System.ComponentModel.DataAnnotations;
using ContentTracker.Entities;

namespace ContentTracker.Models;

public class Movie
{
    [Required]
    public Guid Id { get; set; }
    public List<SourceMovie> Sources { get; set; } = new List<SourceMovie>();

    public static Movie From(MovieEntity e)
    {
        return new Movie
        {
            Id = e.Id,
            Sources = e.Sources.Select(sme => SourceMovie.From(sme)).ToList()
        };
    }
}
