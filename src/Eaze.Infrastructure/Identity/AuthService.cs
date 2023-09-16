using System.Security.Authentication;
using System.Web;
using Eaze.Application.Common.Interfaces;
using Eaze.Application.Mail;
using Eaze.Application.Requests;
using Eaze.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Eaze.Infrastructure.Identity;

public sealed class AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
    IEmailSender emailSender) : IAuthService
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
            if (result.Errors.Any())
            {
                throw new ValidationException(result.Errors.Select(x => new ValidationFailure(x.Code, x.Description)));
            }

            throw new AuthenticationException("Could not create user");
        }

        return user;
    }

    public async Task<string> GeneratePasswordResetToken(User user)
    {
        return await userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task GenerateAndSendEmailConfirmationToken(User user, string url)
    {
        var token = HttpUtility.UrlEncode(await userManager.GenerateEmailConfirmationTokenAsync(user));

        var confirmUrl = $"{url}&token={token}";

        await emailSender.SendAsync(new ConfirmEmail(user, confirmUrl));
    }

    public async Task<User> ConfirmEmail(Guid userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new AuthenticationException("Invalid user");
        }

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            throw new AuthenticationException("Invalid token");
        }

        return user;
    }
    
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}