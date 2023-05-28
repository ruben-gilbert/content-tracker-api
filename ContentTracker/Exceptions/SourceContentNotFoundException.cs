namespace ContentTracker.Exceptions;

/// <summary>
/// When a piece of content cannot be retrieved from a target source by the provided ID.
/// </summary>
public class SourceContentNotFoundException : Exception
{
    public SourceContentNotFoundException() { }

    public SourceContentNotFoundException(string message)
        : base(message) { }

    public SourceContentNotFoundException(string message, Exception inner)
        : base(message, inner) { }
}
