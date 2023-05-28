using ContentTracker.Clients;
using ContentTracker.Entities;
using ContentTracker.Exceptions;
using ContentTracker.Options;
using ContentTracker.Repository;
using ContentTracker.Services;
using ContentTracker.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ContentTracker;

public class Startup
{
    private IConfiguration _configuration;
    private IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // TODO: Authentication/Authorization configuration?

        IConfigurationSection contentTrackerSection = _configuration.GetSection(
            ContentTrackerOptions.ContentTracker
        );
        services.Configure<ContentTrackerOptions>(contentTrackerSection);

        services.AddControllers();

        // TODO: Use an actual database...
        services.AddDbContext<RepositoryContext>(
            options => options.UseInMemoryDatabase("ContentTracker")
        );

        string sourcesSectionName = nameof(ContentTrackerOptions.Sources);
        List<SourceOptions>? sources = contentTrackerSection
            .GetSection(sourcesSectionName)
            .Get<List<SourceOptions>>();

        if (sources == null)
        {
            throw new ConfigurationException(
                $"Could not find '{sourcesSectionName}' section in config file."
            );
        }

        // FIXME: In production app, inject secrets (api keys, etc) from env or secrets manager and instead keep key/var name in config file.
        if (sources.Exists(s => s.Name == Sources.Tmdb))
        {
            services.AddScoped<IMovieClient, TmdbMovieClient>();
        }

        services.AddScoped<IRepository<MovieEntity>, Repository<MovieEntity>>();
        services.AddScoped<
            ISourceRepository<SourceMovieEntity>,
            SourceRepository<SourceMovieEntity>
        >();
        services.AddScoped<IMovieService<MovieEntity>, MovieService>();

        services.AddValidatorsFromAssemblyContaining<SourceValidator>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // TODO: CORS policy, configuration, signalr, etc...
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (_environment.IsProduction())
        {
            app.UseHsts();
        }

        // TODO: Add a error-handling middleware (for auto-injecting ProblemDetails)

        // TODO: Enable CORS

        app.UseRouting();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
