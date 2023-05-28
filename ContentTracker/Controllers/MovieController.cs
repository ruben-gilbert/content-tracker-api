using ContentTracker.Entities;
using ContentTracker.Exceptions;
using ContentTracker.Models;
using ContentTracker.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ContentTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService<MovieEntity> _service;

    public MovieController(IMovieService<MovieEntity> service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Movie>> GetMovie(Guid id)
    {
        try
        {
            MovieEntity m = await _service.GetById(id);
            return Ok(Movie.From(m));
        }
        catch (TrackedContentNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("{id}/renew/{sourceName}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SourceMovie>> RenewMovieSource(Guid id, string sourceName)
    {
        try
        {
            SourceMovieEntity s = await _service.RenewFromSource(id, sourceName);
            return Ok(SourceMovie.From(s));
        }
        catch (InvalidSourceException)
        {
            return UnprocessableEntity();
        }
        catch (TrackedContentNotFoundException)
        {
            return NotFound();
        }
        catch (SourceContentNotCachedException)
        {
            return NotFound();
        }
        catch (SourceRenewedTooSoonException e)
        {
            HttpContext.Response.Headers.Add(HeaderNames.RetryAfter, e.RetryAfter.ToString());
            return Conflict();
        }
        catch (CachedContentNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("source")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<Movie>> AddFromSource(
        [FromBody] Source s,
        [FromServices] IValidator<Source> validator
    )
    {
        try
        {
            await validator.ValidateAndThrowAsync(s);
            MovieEntity m = await _service.AddFromSource(s.SourceName!, s.SourceId);
            return CreatedAtAction(nameof(GetMovie), new { id = m.Id }, Movie.From(m));
        }
        catch (InvalidSourceException)
        {
            return UnprocessableEntity();
        }
        catch (SourceContentNotFoundException)
        {
            return NotFound();
        }
        catch (SourceContentCachedException)
        {
            return Conflict();
        }
    }
}
