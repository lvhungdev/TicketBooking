namespace Domain.Users.Ports;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Compare(string origin, string hashed);
}
