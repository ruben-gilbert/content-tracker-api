namespace ContentTracker.Entities;

public interface ICacheableEntity
{
    public DateTime LastRenewed { get; set; }
}
