using Domain.Movies.Models;
using Domain.Movies.Ports;
using MediatR;

namespace Domain.Movies.UseCases;

public record GetMovieByIdRequest(string Id) : IRequest<Movie?>;

public class GetMovieByIdRequestHandler : IRequestHandler<GetMovieByIdRequest, Movie?>
{
    private readonly IMovieRepository movieRepo;

    public GetMovieByIdRequestHandler(IMovieRepository movieRepo)
    {
        this.movieRepo = movieRepo;
    }

    public Task<Movie?> Handle(GetMovieByIdRequest request, CancellationToken cancellationToken)
    {
        return movieRepo.GetMovieById(request.Id);
    }
}
