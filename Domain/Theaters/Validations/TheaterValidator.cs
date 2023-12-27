using Domain.Theaters.Models;
using FluentValidation;

namespace Domain.Theaters.Validations;

public class TheaterValidator : AbstractValidator<Theater>
{
    public TheaterValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
        RuleFor(m => m.Address).NotEmpty();
    }
}
