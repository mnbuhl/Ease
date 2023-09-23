using BlazorTemplater;
using Ease.App.Mail.Layouts;
using Ease.App.Models;
using Ease.Infrastructure.Mailing;

namespace Ease.App.Mail;

public sealed class ResetPassword(User user, string resetUrl) : Mailable
{
    public override void Build()
    {
        To(user.Email!);
        Subject("Reset your password");

        Body(new ComponentRenderer<Templates.ResetPassword>()
            .Set(c => c.Name, user.Name!)
            .Set(c => c.ResetUrl, resetUrl)
            .UseLayout<MainLayout>()
            .Render());
    }
}