using Domain.Movies.Models;
using Domain.Movies.Ports;
using MediatR;

namespace Domain.Movies.UseCases;

public record GetAllMoviesRequest : IRequest<List<Movie>>;

public class GetAllMoviesRequestHandler(IMovieRepository movieRepo) : IRequestHandler<GetAllMoviesRequest, List<Movie>>
{
    public Task<List<Movie>> Handle(GetAllMoviesRequest request, CancellationToken cancellationToken)
    {
        return movieRepo.GetAllMovies();
    }
}
