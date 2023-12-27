using Domain.Errors;
using Domain.Movies.Models;
using Domain.Movies.Ports;
using Domain.Movies.Validations;
using FluentResults;
using FluentValidation.Results;

namespace Domain.Movies.UseCases;

public class MovieUseCases : IMovieUseCases
{
    private readonly IMovieRepository movieRepo;

    public MovieUseCases(IMovieRepository movieRepo)
    {
        this.movieRepo = movieRepo;
    }

    public Task<List<Movie>> GetAllMovies()
    {
        return movieRepo.GetAllMovies();
    }

    public Task<Movie?> GetMovieById(string id)
    {
        return movieRepo.GetMovieById(id);
    }

    public async Task<Result<Movie>> CreateMovie(Movie movie)
    {
        ValidationResult validationResult = await new MovieValidator().ValidateAsync(movie);
        if (!validationResult.IsValid) return Result.Fail(new ValidationError(validationResult.Errors));

        movie.Id = Guid.NewGuid().ToString();
        movie.CreatedAt = DateTimeOffset.Now;
        movie.UpdatedAt = DateTimeOffset.Now;

        return await movieRepo.CreateMovie(movie);
    }

    public async Task<Result<Movie>> UpdateMovie(Movie movie)
    {
        Movie? existingMovie = await GetMovieById(movie.Id);
        if (existingMovie == null) return Result.Fail(new IdNotFoundError(movie.Id));

        existingMovie.UpdatedAt = DateTimeOffset.Now;

        return await movieRepo.UpdateMovie(existingMovie);
    }

    public Task<Result<string>> DeleteMovie(string id)
    {
        return movieRepo.DeleteMovie(id);
    }
}
