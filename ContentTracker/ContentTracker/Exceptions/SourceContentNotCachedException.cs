namespace ContentTracker.Exceptions;

/// <summary>
/// When a tracked content item does not have a cached source item for a specific source.
/// </summary>
public class SourceContentNotCachedException : Exception
{
    public SourceContentNotCachedException() { }

    public SourceContentNotCachedException(string message)
        : base(message) { }

    public SourceContentNotCachedException(string message, Exception inner)
        : base(message, inner) { }
}
