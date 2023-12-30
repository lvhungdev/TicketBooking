using System.ComponentModel.DataAnnotations;
using Domain.Users.Models;

namespace Infrastructure.Storage.Entities;

public class UserEntity : BaseEntity
{
    [MaxLength(255)] public string Email { get; set; } = null!;
    [MaxLength(255)] public string Password { get; set; } = null!;
    [MaxLength(255)] public string FullName { get; set; } = null!;
    public Role Role { get; set; }

    public User MapToUser()
    {
        return new User
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            Email = Email,
            Password = Password,
            FullName = FullName,
            Role = Role
        };
    }

    public static UserEntity FromUser(User user)
    {
        return new UserEntity
        {
            Id = user.Id,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Email = user.Email,
            Password = user.Password,
            FullName = user.FullName,
            Role = user.Role
        };
    }
}
