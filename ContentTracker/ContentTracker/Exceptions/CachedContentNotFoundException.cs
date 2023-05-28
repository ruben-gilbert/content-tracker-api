namespace ContentTracker.Exceptions;

/// <summary>
/// When a cached content item cannot be found (i.e. is not in the database).
/// </summary>
public class CachedContentNotFoundException : Exception
{
    public CachedContentNotFoundException() { }

    public CachedContentNotFoundException(string message)
        : base(message) { }

    public CachedContentNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
