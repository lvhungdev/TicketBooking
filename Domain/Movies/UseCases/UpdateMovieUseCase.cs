using Domain.Common.Errors;
using Domain.Movies.Models;
using Domain.Movies.Ports;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Domain.Movies.UseCases;

public record UpdateMovieRequest(string Id, string Title, string? Description, int DurationInSecond, Genre Genre)
    : IRequest<Result<Movie>>;

public class UpdateMovieRequestHandler : IRequestHandler<UpdateMovieRequest, Result<Movie>>
{
    private readonly IMovieRepository movieRepo;

    public UpdateMovieRequestHandler(IMovieRepository movieRepo)
    {
        this.movieRepo = movieRepo;
    }

    public async Task<Result<Movie>> Handle(UpdateMovieRequest request, CancellationToken cancellationToken)
    {
        Movie? existingMovie = await movieRepo.GetMovieById(request.Id);
        if (existingMovie == null) return Result.Fail(new IdNotFoundError(request.Id));

        existingMovie.UpdatedAt = DateTimeOffset.Now;
        existingMovie.Title = request.Title;
        existingMovie.Description = request.Description;
        existingMovie.DurationInSecond = request.DurationInSecond;
        existingMovie.Genre = request.Genre;

        Result<Movie> updatedMovieResult = await movieRepo.UpdateMovie(existingMovie);
        if (updatedMovieResult.IsFailed) return updatedMovieResult;

        await movieRepo.SaveChanges();

        return existingMovie;
    }
}

public class UpdateMovieRequestValidator : AbstractValidator<UpdateMovieRequest>
{
    public UpdateMovieRequestValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
        RuleFor(m => m.DurationInSecond).GreaterThan(0);
    }
}
