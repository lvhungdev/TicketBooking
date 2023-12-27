using FluentResults;
using FluentValidation.Results;

namespace Domain.Errors;

public class ValidationError : Error
{
    public ValidationError(IEnumerable<string> errors) : base("Validation Failure")
    {
        Reasons.AddRange(errors.Select(err => (IError)new Error(err)).ToList());
    }

    public ValidationError(IEnumerable<ValidationFailure> errors) : base("Validation Failure")
    {
        Reasons.AddRange(errors.Select(err => (IError)new Error(err.ToString())).ToList());
    }
}
