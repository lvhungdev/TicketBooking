using Domain.Common.Behaviors;
using Domain.Common.Errors;
using Domain.Movies.Models;
using Domain.Movies.Ports;
using Domain.Users.Models;
using FluentResults;
using MediatR;

namespace Domain.Movies.UseCases;

public record RemoveMovieRequest(string Id) : IAuthorizedRequest<Result<string>>
{
    private readonly List<Role> requiredRoles = new() { Role.Admin };

    public List<Role> GetRequiredRoles()
    {
        return requiredRoles;
    }
}

public class RemoveMovieRequestHandler : IRequestHandler<RemoveMovieRequest, Result<string>>
{
    private readonly IMovieRepository movieRepo;

    public RemoveMovieRequestHandler(IMovieRepository movieRepo)
    {
        this.movieRepo = movieRepo;
    }

    public async Task<Result<string>> Handle(RemoveMovieRequest request, CancellationToken cancellationToken)
    {
        Movie? movie = await movieRepo.GetMovieById(request.Id);
        if (movie == null) return Result.Fail(new IdNotFoundError(request.Id));

        Result<string> deletedResult = await movieRepo.DeleteMovie(request.Id);
        if (deletedResult.IsFailed) return deletedResult;

        await movieRepo.SaveChanges();

        return request.Id;
    }
}
