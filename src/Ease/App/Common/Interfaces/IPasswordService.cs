using Ease.App.Models;

namespace Ease.App.Common.Interfaces;

public interface IPasswordService
{
    Task SendPasswordReset(User user, string url);
}