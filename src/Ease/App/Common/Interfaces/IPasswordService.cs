using Ease.App.Models;

namespace Ease.App.Common.Interfaces;

public interface IPasswordService
{
    Task<User?> SendPasswordReset(string email, string url);
    Task ResetPassword(string email, string token, string password);
}