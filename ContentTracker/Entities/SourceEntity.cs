using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ContentTracker.Entities;

[PrimaryKey(nameof(SourceName), nameof(SourceId))]
public class SourceEntity : ISourceEntity, ICacheableEntity
{
    [Required]
    public required int SourceId { get; set; }

    [Required]
    public required string SourceName { get; set; }

    [Required]
    public DateTime LastRenewed { get; set; }
}
