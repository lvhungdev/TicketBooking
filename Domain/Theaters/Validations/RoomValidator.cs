using Domain.Theaters.Models;
using FluentValidation;

namespace Domain.Theaters.Validations;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(m => m.Title).NotEmpty();
    }
}
