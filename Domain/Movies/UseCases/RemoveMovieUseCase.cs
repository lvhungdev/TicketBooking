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
    private readonly List<Role> requiredRoles = [Role.Admin];

    public List<Role> GetRequiredRoles()
    {
        return requiredRoles;
    }
}

public class RemoveMovieRequestHandler(IMovieRepository movieRepo) : IRequestHandler<RemoveMovieRequest, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveMovieRequest request, CancellationToken cancellationToken)
    {
        Movie? movie = await movieRepo.GetMovieById(request.Id);
        if (movie == null)
        {
            return Result.Fail(new IdNotFoundError(request.Id));
        }

        Result<string> deletedResult = await movieRepo.DeleteMovie(request.Id);
        if (deletedResult.IsFailed)
        {
            return deletedResult;
        }

        await movieRepo.SaveChanges();

        return request.Id;
    }
}
