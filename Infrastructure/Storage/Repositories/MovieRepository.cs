using Domain.Common.Errors;
using Domain.Movies.Models;
using Domain.Movies.Ports;
using FluentResults;
using Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class MovieRepository(AppDbContext dbContext) : IMovieRepository
{
    public async Task<List<Movie>> GetAllMovies()
    {
        List<MovieEntity> movies = await dbContext.Movies.ToListAsync();
        return movies.Select(m => m.MapToMovie()).ToList();
    }

    public async Task<Movie?> GetMovieById(string id)
    {
        MovieEntity? movie = await dbContext.Movies.FindAsync(id);
        return movie?.MapToMovie();
    }

    public Task<Result<Movie>> CreateMovie(Movie movie)
    {
        dbContext.Movies.Add(MovieEntity.FromMovie(movie));

        return Task.FromResult(Result.Ok(movie));
    }

    public async Task<Result<Movie>> UpdateMovie(Movie movie)
    {
        MovieEntity? toBeUpdatedMovie = await dbContext.Movies.FindAsync(movie.Id);
        if (toBeUpdatedMovie == null)
        {
            return Result.Fail(new IdNotFoundError(movie.Id));
        }

        toBeUpdatedMovie.Title = movie.Title;
        toBeUpdatedMovie.UpdatedAt = movie.UpdatedAt;
        toBeUpdatedMovie.Title = movie.Title;
        toBeUpdatedMovie.Description = movie.Description;
        toBeUpdatedMovie.DurationInSecond = movie.DurationInSecond;
        toBeUpdatedMovie.Genre = movie.Genre;

        return Result.Ok(movie);
    }

    public async Task<Result<string>> DeleteMovie(string id)
    {
        MovieEntity? toBeDeletedMovie = await dbContext.Movies.FindAsync(id);
        if (toBeDeletedMovie == null)
        {
            return Result.Fail(new IdNotFoundError(id));
        }

        dbContext.Movies.Remove(toBeDeletedMovie);

        return Result.Ok(id);
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
