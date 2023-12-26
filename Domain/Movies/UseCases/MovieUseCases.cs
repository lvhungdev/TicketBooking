using Domain.Movies.Models;
using FluentResults;

namespace Domain.Movies.UseCases;

public class MovieUseCases : IMovieUseCases
{
    public Task<List<Movie>> GetAllMovies()
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> GetMovieById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Movie>> CreateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Movie>> UpdateMovie(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> DeleteMovie(string id)
    {
        throw new NotImplementedException();
    }
}
