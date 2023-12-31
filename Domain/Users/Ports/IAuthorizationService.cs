using Domain.Users.Models;

namespace Domain.Users.Ports;

public interface IAuthorizationService
{
    Role? GetCurrentRole();
    void SetCurrentRole(Role? role);
}
