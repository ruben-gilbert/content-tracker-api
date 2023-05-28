using ContentTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentTracker.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieEntity>().ToTable("Movies");
        modelBuilder
            .Entity<MovieEntity>()
            .HasMany(e => e.Sources)
            .WithOne()
            .HasForeignKey(e => e.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SourceMovieEntity>().ToTable("SourceMovies");
    }
}
