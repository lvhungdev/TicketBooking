using FluentResults;
using FluentValidation.Results;

namespace Domain.Errors;

public class ValidationError : IError
{
    public ValidationError(IEnumerable<string> errors)
    {
        Reasons = errors.Select(err => (IError)new Error(err)).ToList();
    }

    public ValidationError(IEnumerable<ValidationFailure> errors)
    {
        Reasons = errors.Select(err => (IError)new Error(err.ToString())).ToList();
    }

    public string Message { get; } = "Validation Failure";
    public Dictionary<string, object> Metadata { get; } = new();
    public List<IError> Reasons { get; }
}
