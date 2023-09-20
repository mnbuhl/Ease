using FluentValidation;

namespace Ease.Web.Requests;

public record NewPasswordRequest(string CurrentPassword, string Password, string PasswordConfirmation);

public class NewPasswordValidator : AbstractValidator<NewPasswordRequest>
{
    public NewPasswordValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.PasswordConfirmation)
            .WithMessage("Passwords do not match");
    }
}