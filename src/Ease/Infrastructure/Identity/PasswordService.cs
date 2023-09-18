using System.Web;
using Ease.App.Common.Interfaces;
using Ease.App.Mail;
using Ease.App.Models;
using Microsoft.AspNetCore.Identity;

namespace Ease.Infrastructure.Identity;

public sealed class PasswordService(UserManager<User> userManager, IEmailSender emailSender) : IPasswordService
{
    public async Task SendPasswordReset(User user, string url)
    {
        var token = HttpUtility.UrlEncode(await userManager.GeneratePasswordResetTokenAsync(user));

        var resetUrl = $"{url}&token={token}";

        await emailSender.SendAsync(new ResetPassword(user, resetUrl));
    }
}