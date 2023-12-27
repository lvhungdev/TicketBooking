using Domain.Movies.Models;
using Domain.Movies.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/movie")]
public class MovieController : ControllerBase
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
}
