using System.Security.Claims;
using Domain.Users.Models;
using Domain.Users.Ports;

namespace API.Authorization;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAuthorizationService authorizationService)
    {
        if (Enum.TryParse(httpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Role)?.Value, out Role r))
        {
            authorizationService.SetCurrentRole(r);
        }

        await next(httpContext);
    }
}

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationService(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationMiddleware>();
    }
}
