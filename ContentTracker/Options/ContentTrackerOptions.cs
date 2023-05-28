namespace ContentTracker.Options;

public class ContentTrackerOptions
{
    public const string ContentTracker = "ContentTracker";

    public required List<SourceOptions> Sources { get; set; }
}
