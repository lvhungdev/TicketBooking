using Domain.Movies.Models;
using Domain.Movies.Ports;
using MediatR;

namespace Domain.Movies.UseCases;

public record GetAllMoviesRequest : IRequest<List<Movie>>;

public class GetAllMoviesRequestHandler : IRequestHandler<GetAllMoviesRequest, List<Movie>>
{
    private readonly IMovieRepository movieRepo;

    public GetAllMoviesRequestHandler(IMovieRepository movieRepo)
    {
        this.movieRepo = movieRepo;
    }

    public Task<List<Movie>> Handle(GetAllMoviesRequest request, CancellationToken cancellationToken)
    {
        return movieRepo.GetAllMovies();
    }
}
