using BlazorTemplater;
using Eaze.App.Helpers;
using Eaze.App.Mail.Layouts;
using Eaze.App.Models;

namespace Eaze.App.Mail;

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