namespace ContentTracker.Entities;

public interface ISourceEntity
{
    // NOTE: Broad assumption that most 3rd party APIs use int's for IDs.
    public int SourceId { get; set; }
    public string SourceName { get; set; }
}
