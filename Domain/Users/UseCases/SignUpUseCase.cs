using Domain.Common.Errors;
using Domain.Users.Models;
using Domain.Users.Ports;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Domain.Users.UseCases;

public record SignUpRequest(string Email, string Password, string FullName) : IRequest<Result<User>>;

public class SignUpRequestHandler(IUserRepository userRepo, IPasswordHasher passwordHasher)
    : IRequestHandler<SignUpRequest, Result<User>>
{
    public async Task<Result<User>> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        User? existingUser = await userRepo.GetUserByEmail(request.Email);
        if (existingUser != null)
        {
            return Result.Fail(new EmailExistedError(request.Email));
        }

        User user =
            new()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
                Email = request.Email,
                Password = passwordHasher.Hash(request.Password),
                FullName = request.FullName,
                Role = Role.User
            };

        await userRepo.AddUser(user);
        await userRepo.SaveChanges();

        return user;
    }
}

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(m => m.Email).EmailAddress();
        RuleFor(m => m.Password.Length).GreaterThanOrEqualTo(8);
        RuleFor(m => m.FullName).NotEmpty();
    }
}
