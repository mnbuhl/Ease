using Eaze.App.Models;

namespace Eaze.App.Common.Interfaces;

public interface IVerifyEmailService
{
    Task SendEmailConfirmation(User user, string url);
    Task<User> ConfirmEmail(Guid userId, string token);
}