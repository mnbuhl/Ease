using BlazorTemplater;
using Eaze.Application.Common.Models;
using Eaze.Application.Mail.Layouts;
using Eaze.Domain.Models;

namespace Eaze.Application.Mail;

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