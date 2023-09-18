using Eaze.App.Common.Helpers;
using Eaze.App.Models;

namespace Eaze.App.Mail;

public sealed class ResetPassword : Mailable
{
    public ResetPassword(User user, string resetUrl)
    {
    }
}