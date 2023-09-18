using Eaze.App.Models;
using Eaze.App.Requests;

namespace Eaze.App.Common.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginRequest loginRequest);
    Task<User> Register(RegisterRequest request);
    Task Logout();
}