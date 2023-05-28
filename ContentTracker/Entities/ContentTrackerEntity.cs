using System.ComponentModel.DataAnnotations;

namespace ContentTracker.Entities;

public class ContentTrackerEntity : IContentTrackerEntity
{
    [Required]
    public required Guid Id { get; set; }
}
