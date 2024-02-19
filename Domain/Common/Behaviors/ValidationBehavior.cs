using Domain.Common.Errors;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Domain.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResultBase
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (validator == null)
        {
            return await next();
        }

        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return (dynamic)Result.Fail(new ValidationFailedError(validationResult.Errors));
        }

        return await next();
    }
}
