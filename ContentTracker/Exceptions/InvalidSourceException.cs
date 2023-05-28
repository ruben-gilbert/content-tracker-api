namespace ContentTracker.Exceptions;

/// <summary>
/// When a service is attempting to use a source that is not defined or available.
/// </summary>
public class InvalidSourceException : Exception
{
    public InvalidSourceException() { }

    public InvalidSourceException(string message)
        : base(message) { }

    public InvalidSourceException(string message, Exception inner)
        : base(message, inner) { }
}
