using Ease.App.Models;
using Ease.App.Requests;

namespace Ease.App.Common.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginRequest loginRequest);
    Task<User> Register(RegisterRequest request);
    Task Logout();
}