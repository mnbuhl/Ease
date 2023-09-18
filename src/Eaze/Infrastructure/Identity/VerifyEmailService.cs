using System.Security.Authentication;
using System.Web;
using Eaze.App.Common.Interfaces;
using Eaze.App.Mail;
using Eaze.App.Models;
using Microsoft.AspNetCore.Identity;

namespace Eaze.Infrastructure.Identity;

public sealed class VerifyEmailService(UserManager<User> userManager, IEmailSender emailSender) : IVerifyEmailService
{
    public async Task SendEmailConfirmation(User user, string url)
    {
        var token = HttpUtility.UrlEncode(await userManager.GenerateEmailConfirmationTokenAsync(user));

        var confirmUrl = $"{url}&token={token}";

        await emailSender.SendAsync(new ConfirmEmail(user, confirmUrl));
    }

    public async Task<User> ConfirmEmail(Guid userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new AuthenticationException("Invalid user");
        }

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            throw new AuthenticationException("Invalid token");
        }

        return user;
    }
}