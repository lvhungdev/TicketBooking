using Domain.Users.Models;
using Domain.Users.Ports;
using FluentValidation;
using MediatR;

namespace Domain.Users.UseCases;

public record SignInRequest(string Email, string Password) : IRequest<User?>;

public class SignInRequestHandler : IRequestHandler<SignInRequest, User?>
{
    private readonly IPasswordHasher passwordHasher;
    private readonly IUserRepository userRepo;

    public SignInRequestHandler(IUserRepository userRepo, IPasswordHasher passwordHasher)
    {
        this.userRepo = userRepo;
        this.passwordHasher = passwordHasher;
    }

    public async Task<User?> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        User? user = await userRepo.GetUserByEmail(request.Email);
        if (user?.Password == null) return null;

        return !passwordHasher.Compare(request.Password, user.Password) ? null : user;
    }
}

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(m => m.Email).EmailAddress();
        RuleFor(m => m.Password.Length).GreaterThanOrEqualTo(8);
    }
}
