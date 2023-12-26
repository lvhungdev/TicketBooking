using Domain.Movies.Models;
using FluentResults;

namespace Domain.Movies.UseCases;

public interface IMovieUseCases
{
    Task<List<Movie>> GetAllMovies();
    Task<Movie?> GetMovieById(string id);
    Task<Result<Movie>> CreateMovie(Movie movie);
    Task<Result<Movie>> UpdateMovie(Movie movie);
    Task<Result<string>> DeleteMovie(string id);
}