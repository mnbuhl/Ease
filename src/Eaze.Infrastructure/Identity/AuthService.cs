using Eaze.Application.Common.Interfaces;
using Eaze.Application.Features.Auth;
using Eaze.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Eaze.Infrastructure.Identity;

public sealed class AuthService(UserManager<User> userManager, SignInManager<User> signInManager) : IAuthService
{
    public async Task<User> Login(LoginRequest loginRequest)
    {
        var user = await userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            throw new Exception("Invalid email or password");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

        if (!result.Succeeded)
        {
            throw new Exception("Invalid email or password");
        }

        return user;
    }
}