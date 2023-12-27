using FluentResults;

namespace Domain.Errors;

public class DatabaseError : Error
{
    public DatabaseError(Exception exception) : base(exception.Message)
    {
    }
}
