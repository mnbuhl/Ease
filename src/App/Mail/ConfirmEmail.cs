using BlazorTemplater;
using Ease.App.Mail.Layouts;
using Ease.App.Models;
using Ease.Infrastructure.Mailing;

namespace Ease.App.Mail;

public sealed class ConfirmEmail(User user, string callbackUrl) : Mailable
{
    public override void Build()
    {
        To(user.Email!);
        Subject("Please confirm your email");

        Body(new ComponentRenderer<Templates.ConfirmEmail>()
            .Set(c => c.Name, user.Name!)
            .Set(c => c.CallbackUrl, callbackUrl)
            .UseLayout<MainLayout>()
            .Render());
    }
}