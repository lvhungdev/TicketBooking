using FluentResults;
using FluentValidation.Results;

namespace Domain.Common.Errors;

public class ValidationFailedError : Error
{
    public ValidationFailedError(IEnumerable<string> errors)
        : base("Validation Failure")
    {
        Reasons.AddRange(errors.Select(err => (IError)new Error(err)).ToList());
    }

    public ValidationFailedError(IEnumerable<ValidationFailure> errors)
        : base("Validation Failure")
    {
        Reasons.AddRange(errors.Select(err => (IError)new Error(err.ToString())).ToList());
    }
}
