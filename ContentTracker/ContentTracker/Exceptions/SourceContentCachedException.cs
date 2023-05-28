namespace ContentTracker.Exceptions;

/// <summary>
/// When a source content item has already been cached in the system and is being
/// tracked.  Typically indicates that the source can be renewed, rather than added.
/// </summary>
public class SourceContentCachedException : Exception
{
    public SourceContentCachedException() { }

    public SourceContentCachedException(string message)
        : base(message) { }

    public SourceContentCachedException(string message, Exception inner)
        : base(message, inner) { }
}
