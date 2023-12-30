using Domain.Movies.Models;
using Domain.Movies.Ports;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Domain.Movies.UseCases;

public record AddMovieRequest(string Title, string? Description, int DurationInSecond, Genre Genre) : IRequest<Result<Movie>>;

public class AddMovieRequestHandler : IRequestHandler<AddMovieRequest, Result<Movie>>
{
    private readonly IMovieRepository movieRepo;

    public AddMovieRequestHandler(IMovieRepository movieRepo)
    {
        this.movieRepo = movieRepo;
    }

    public async Task<Result<Movie>> Handle(AddMovieRequest request, CancellationToken cancellationToken)
    {
        Movie movie = new()
        {
            Id = Guid.NewGuid().ToString(),
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
            Title = request.Title,
            Description = request.Description,
            DurationInSecond = request.DurationInSecond,
            Genre = request.Genre
        };

        Result<Movie> createdMovieResult = await movieRepo.CreateMovie(movie);
        if (createdMovieResult.IsFailed) return createdMovieResult;

        await movieRepo.SaveChanges();

        return movie;
    }
}

public class AddMovieRequestValidator : AbstractValidator<AddMovieRequest>
{
    public AddMovieRequestValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
        RuleFor(m => m.DurationInSecond).GreaterThan(0);
    }
}
