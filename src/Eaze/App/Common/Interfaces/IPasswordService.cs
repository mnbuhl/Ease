using Eaze.App.Models;

namespace Eaze.App.Common.Interfaces;

public interface IPasswordService
{
    Task SendPasswordReset(User user, string url);
}