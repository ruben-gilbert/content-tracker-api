namespace ContentTracker.Exceptions;

/// <summary>
/// When an attempt to renew a source item happens too soon after it was last renewed.
/// </summary>
public class SourceRenewedTooSoonException : Exception
{
    private int _retryAfter;

    public int RetryAfter
    {
        get { return _retryAfter; }
    }

    public SourceRenewedTooSoonException(int retryAfter)
    {
        _retryAfter = retryAfter;
    }

    public SourceRenewedTooSoonException(int retryAfter, string message)
        : base(message)
    {
        _retryAfter = retryAfter;
    }

    public SourceRenewedTooSoonException(int retryAfter, string message, Exception inner)
        : base(message, inner)
    {
        _retryAfter = retryAfter;
    }
}
