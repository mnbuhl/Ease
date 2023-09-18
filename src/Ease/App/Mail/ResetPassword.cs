using BlazorTemplater;
using Ease.App.Common.Helpers;
using Ease.App.Mail.Layouts;
using Ease.App.Models;

namespace Ease.App.Mail;

public sealed class ResetPassword : Mailable
{
    public ResetPassword(User user, string resetUrl)
    {
        To = user.Email!;
        Subject = "Please confirm your email";

        Body = new ComponentRenderer<Templates.ResetPassword>()
            .Set(c => c.Name, user.Name!)
            .Set(c => c.ResetUrl, resetUrl)
            .UseLayout<MainLayout>()
            .Render();
    }
}