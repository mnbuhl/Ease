using BlazorTemplater;
using Ease.App.Common.Helpers;
using Ease.App.Mail.Layouts;
using Ease.App.Models;

namespace Ease.App.Mail;

public sealed class ConfirmEmail : Mailable
{
    public ConfirmEmail(User user, string callbackUrl)
    {
        To = user.Email!;
        Subject = "Please confirm your email";

        Body = new ComponentRenderer<Templates.ConfirmEmail>()
            .Set(c => c.Name, user.Name!)
            .Set(c => c.CallbackUrl, callbackUrl)
            .UseLayout<MainLayout>()
            .Render();
    }
}