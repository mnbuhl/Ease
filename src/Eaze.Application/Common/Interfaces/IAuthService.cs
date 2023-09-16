using Eaze.Application.Features.Auth;
using Eaze.Domain.Models;

namespace Eaze.Application.Common.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginRequest loginRequest);
}