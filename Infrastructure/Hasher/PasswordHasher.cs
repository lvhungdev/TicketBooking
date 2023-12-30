using Domain.Users.Ports;

namespace Infrastructure.Hasher;

// TODO Implement this
public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return password;
    }

    public bool Compare(string origin, string hashed)
    {
        return origin == hashed;
    }
}
