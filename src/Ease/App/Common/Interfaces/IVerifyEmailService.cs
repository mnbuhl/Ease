using Ease.App.Models;

namespace Ease.App.Common.Interfaces;

public interface IVerifyEmailService
{
    Task SendEmailConfirmation(User user, string url);
    Task<User> ConfirmEmail(Guid userId, string token);
}