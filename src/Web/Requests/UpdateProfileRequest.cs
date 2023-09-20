using FluentValidation;

namespace Ease.Web.Requests;

public record UpdateProfileRequest(string Name, string Email);

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}