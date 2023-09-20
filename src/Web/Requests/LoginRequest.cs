using FluentValidation;

namespace Ease.Web.Requests;

public record LoginRequest(string Email, string Password, bool Remember);

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}