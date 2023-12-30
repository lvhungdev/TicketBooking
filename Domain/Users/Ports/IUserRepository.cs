using Domain.Users.Models;

namespace Domain.Users.Ports;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(string id);
    Task<User?> GetUserByEmail(string email);
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task SaveChanges();
}
