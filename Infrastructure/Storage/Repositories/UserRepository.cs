using Domain.Users.Models;
using Domain.Users.Ports;
using Infrastructure.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<List<User>> GetAllUsers()
    {
        List<UserEntity> users = await dbContext.Users.ToListAsync();

        return users.ConvertAll(m => m.MapToUser());
    }

    public async Task<User?> GetUserById(string id)
    {
        UserEntity? user = await dbContext.Users.FindAsync(id);

        return user?.MapToUser();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        UserEntity? user = await dbContext.Users.Where(m => m.Email == email).FirstOrDefaultAsync();

        return user?.MapToUser();
    }

    public Task<User> AddUser(User user)
    {
        dbContext.Users.Add(UserEntity.FromUser(user));

        return Task.FromResult(user);
    }

    public async Task<User> UpdateUser(User user)
    {
        UserEntity userEntity = (await dbContext.Users.FindAsync(user.Id))!;

        userEntity.CreatedAt = user.CreatedAt;
        userEntity.UpdatedAt = user.UpdatedAt;
        userEntity.Email = user.Email;
        userEntity.Password = user.Password;
        userEntity.FullName = user.FullName;
        userEntity.Role = user.Role;

        return user;
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}
