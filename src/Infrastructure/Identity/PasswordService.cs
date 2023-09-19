using System.Web;
using Ease.App.Common.Interfaces;
using Ease.App.Mail;
using Ease.App.Models;
using Microsoft.AspNetCore.Identity;

namespace Ease.Infrastructure.Identity;

public sealed class PasswordService(UserManager<User> userManager, IEmailSender emailSender) : IPasswordService
{
    public async Task<User?> SendPasswordReset(string email, string url)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return null;
        }
        
        var token = HttpUtility.UrlEncode(await userManager.GeneratePasswordResetTokenAsync(user));

        var resetUrl = $"{url}&token={token}";

        await emailSender.SendAsync(new ResetPassword(user, resetUrl));

        return user;
    }

    public async Task ResetPassword(string email, string token, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return;
        }

        await userManager.ResetPasswordAsync(user, token, password);
    }

    public async Task ChangePassword(User user, string currentPassword, string newPassword)
    {
        await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }
}