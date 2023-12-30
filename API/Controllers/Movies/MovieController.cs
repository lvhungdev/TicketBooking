using API.Controllers.Movies.Dtos;
using Domain.Movies.Models;
using Domain.Movies.UseCases;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Movies;

[Route("api/movie")]
public class MovieController : ApiController
{
    private readonly IMediator mediator;

    public MovieController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        GetAllMoviesRequest req = new();
        List<Movie> movies = await mediator.Send(req);

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(string id)
    {
        GetMovieByIdRequest req = new(id);
        Movie? movie = await mediator.Send(req);

        if (movie == null) return new NotFoundResult();

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieReqDto dto)
    {
        AddMovieRequest req = new(dto.Title, dto.Description, dto.DurationInSecond, dto.Genre);
        Result<Movie> movieResult = await mediator.Send(req);

        return movieResult.IsFailed ? Problem(movieResult.Errors) : Ok(movieResult.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(string id, [FromBody] UpdateMovieReqDto dto)
    {
        UpdateMovieRequest req = new(id, dto.Title, dto.Description, dto.DurationInSecond, dto.Genre);
        Result<Movie> movieResult = await mediator.Send(req);

        return movieResult.IsFailed ? Problem(movieResult.Errors) : Ok(movieResult.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMove(string id)
    {
        RemoveMovieRequest req = new(id);
        Result<string> result = await mediator.Send(req);

        return result.IsFailed ? Problem(result.Errors) : Ok();
    }
}
