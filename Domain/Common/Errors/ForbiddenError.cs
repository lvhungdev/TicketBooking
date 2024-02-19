using FluentResults;

namespace Domain.Common.Errors;

public class ForbiddenError : Error
{
    public ForbiddenError()
        : base("Forbidden") { }
}
