using Ease.App.Common.Helpers;
using Ease.App.Models;

namespace Ease.App.Mail;

public sealed class ResetPassword : Mailable
{
    public ResetPassword(User user, string resetUrl)
    {
    }
}