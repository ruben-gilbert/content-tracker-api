using System.ComponentModel.DataAnnotations;

namespace ContentTracker.Models;

public class Source
{
    [Required]
    public int SourceId { get; set; }

    [Required]
    public string? SourceName { get; set; }
}
