namespace ContentTracker.Entities;

public interface IMovieEntity : IContentTrackerEntity
{
    public IList<SourceMovieEntity> Sources { get; }
}
