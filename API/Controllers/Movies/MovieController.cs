using API.Controllers.Movies.Dtos;
using Domain.Movies.Models;
using Domain.Movies.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Movies;

[Route("api/movie")]
public class MovieController : ApiController
{
    private readonly IMovieUseCases movieUseCases;

    public MovieController(IMovieUseCases movieUseCases)
    {
        this.movieUseCases = movieUseCases;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        List<Movie> movies = await movieUseCases.GetAllMovies();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(string id)
    {
        Movie? movie = await movieUseCases.GetMovieById(id);

        if (movie == null)
        {
            return new NotFoundResult();
        }

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieReqDto dto)
    {
        Result<Movie> movieResult = await movieUseCases.CreateMovie(dto.MapToMovie());

        return movieResult.IsFailed ? Problem(movieResult.Errors) : Ok(movieResult.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(string id, [FromBody] UpdateMovieReqDto dto)
    {
        Movie movie = dto.MapToMovie();
        movie.Id = id;

        Result<Movie> movieResult = await movieUseCases.UpdateMovie(movie);

        return movieResult.IsFailed ? Problem(movieResult.Errors) : Ok(movieResult.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMove(string id)
    {
        Result<string> result = await movieUseCases.DeleteMovie(id);

        return result.IsFailed ? Problem(result.Errors) : Ok();
    }
}
