using FluentResults;

namespace Domain.Errors;

public class IdNotFoundError : IError
{
    public IdNotFoundError()
    {
        Reasons.Add(new Error(Message));
    }

    public string Message { get; } = "Id Not Found";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; } = new();
}
