using System.Security.Claims;
using Domain.Users.Models;
using Domain.Users.Ports;

namespace API.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private Role? role;

    public Role? GetCurrentRole()
    {
        return role;
    }

    public void SetCurrentRole(Role? r)
    {
        role = r;
    }

    public void SetRole(ClaimsPrincipal principal)
    {
        if (Enum.TryParse(principal.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Role)?.Value, out Role r))
        {
            role = r;
        }
    }
}
