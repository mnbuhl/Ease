using Eaze.Application.Common.Interfaces;
using Eaze.Application.Common.Models;
using Eaze.Application.Mail;
using Eaze.Application.Requests;
using Eaze.Domain.Constants;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

public sealed class AuthController(IAuthService authService, IEmailSender emailSender) : Controller
{
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        
        return Inertia.Render("Auth/Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        await authService.Login(request);

        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        
        return Inertia.Render("Auth/Register");
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Register();
        }

        var user = await authService.Register(request);

        var token = await authService.GenerateEmailConfirmationToken(user);

        var url = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token }, Request.Scheme)!;

        await emailSender.SendAsync(new ConfirmEmail(user, url));
        
        await authService.Login(new LoginRequest(request.Email, request.Password, true));

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await authService.Logout();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
    {
        await authService.ConfirmEmail(userId, token);

        Inertia.Share("toast", new Toast("Thank you for confirming your email address.", ToastType.Success));

        bool isAuthenticated = User.Identity?.IsAuthenticated == true;

        return RedirectToAction("Index", isAuthenticated ? "Dashboard" : "Home");
    }
}