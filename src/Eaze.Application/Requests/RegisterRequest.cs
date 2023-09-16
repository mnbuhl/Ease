using FluentValidation;

namespace Eaze.Application.Requests;

public record RegisterRequest(string Email, string Name, string Password, string PasswordConfirmation);

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(250);

        RuleFor(x => x.Password).NotEmpty()
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

        RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.Password);
    }
}