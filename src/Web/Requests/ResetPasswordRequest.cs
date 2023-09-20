using FluentValidation;

namespace Ease.Web.Requests;

public record ResetPasswordRequest(string Email, string Token, string Password, string PasswordConfirmation);

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.PasswordConfirmation)
            .WithMessage("Passwords do not match");
    }
}