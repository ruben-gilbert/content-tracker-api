namespace ContentTracker.Exceptions;

/// <summary>
/// When a tracked content item does not exist with a provided ID.
/// </summary>
public class TrackedContentNotFoundException : Exception
{
    public TrackedContentNotFoundException() { }

    public TrackedContentNotFoundException(string message)
        : base(message) { }

    public TrackedContentNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
