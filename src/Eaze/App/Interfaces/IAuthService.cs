using Eaze.App.Models;
using Eaze.App.Requests;

namespace Eaze.App.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginRequest loginRequest);
    Task<User> Register(RegisterRequest request);
    Task Logout();
    Task<string> GeneratePasswordResetToken(User user);
    Task GenerateAndSendEmailConfirmationToken(User user, string url);
    Task<User> ConfirmEmail(Guid userId, string token);
}