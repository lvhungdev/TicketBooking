using Domain.Movies.Models;
using FluentValidation;

namespace Domain.Movies.Validations;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
        RuleFor(m => m.DurationInSecond).GreaterThan(0);
    }
}
