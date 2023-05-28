namespace ContentTracker.Options;

public class SourceOptions
{
    public required string Name { get; set; }
    public string ApiKey { get; set; } = String.Empty;
    public string ApiSecret { get; set; } = String.Empty;
    public int RenewalDelay { get; set; } = 60;
}
