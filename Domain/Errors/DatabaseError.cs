using FluentResults;

namespace Domain.Errors;

public class DatabaseError : IError
{
    public DatabaseError(Exception exception)
    {
        Message = exception.Message;
        Reasons.Add(new Error(exception.Message));
    }

    public string Message { get; }
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}
