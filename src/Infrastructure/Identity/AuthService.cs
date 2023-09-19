using System.Security.Authentication;
using Ease.App.Common.Interfaces;
using Ease.App.Constants;
using Ease.App.Models;
using Ease.App.Requests;
using Microsoft.AspNetCore.Identity;

namespace Ease.Infrastructure.Identity;

public sealed class AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<User> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new AuthenticationException("Invalid email or password");
        }

        var result = await signInManager.PasswordSignInAsync(user, request.Password, request.Remember, false);

        if (!result.Succeeded)
        {
            throw new AuthenticationException("Invalid email or password");
        }

        return user;
    }

    public async Task<User> Register(RegisterRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            logger.LogError("Could not create user {@User}: {@Errors}", user, result.Errors);
            throw new AuthenticationException("Could not create user");
        }

        result = await userManager.AddToRoleAsync(user, Role.User);

        if (!result.Succeeded)
        {
            logger.LogError("Could not add user {@User} to role {Role}: {@Errors}", user, Role.User, result.Errors);
            throw new AuthenticationException("Could not create user");
        }

        return user;
    }
    
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}