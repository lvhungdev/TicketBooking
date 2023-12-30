using Domain.Common.Models;

namespace Domain.Users.Models;

public class User : BaseModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Role Role { get; set; } = Role.User;
}

public enum Role
{
    Admin,
    User
}
