using Domain.Common.Errors;
using Domain.Users.Models;
using Domain.Users.Ports;
using FluentResults;
using MediatR;

namespace Domain.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuthorizedRequest<TResponse>
    where TResponse : IResultBase
{
    private readonly IAuthorizationService authorizationService;

    public AuthorizationBehavior(IAuthorizationService authorizationService)
    {
        this.authorizationService = authorizationService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.GetRequiredRoles().Count == 0) return await next();

        Role? currentRole = authorizationService.GetCurrentRole();

        if (currentRole == null) return (dynamic)Result.Fail(new ForbiddenError());

        if (!request.GetRequiredRoles().Contains((Role)currentRole)) return (dynamic)Result.Fail(new ForbiddenError());

        return await next();
    }
}

public interface IAuthorizedRequest<out T> : IRequest<T>
{
    List<Role> GetRequiredRoles();
}
