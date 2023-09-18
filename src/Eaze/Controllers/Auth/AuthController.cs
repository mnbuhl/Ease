using Eaze.App.Common.Interfaces;
using Eaze.App.Requests;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Controllers.Auth;

public sealed class AuthController(IAuthService authService, IVerifyEmailService verifyEmailService,
    ILogger<AuthController> logger) : BaseController
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
        try
        {
            await authService.Login(request);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("email", "Invalid email or password.");
            ModelState.AddModelError("password", "Invalid email or password.");
            logger.LogError(ex, "Error logging in");
            return Login();
        }

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

        string url = Url.Action("Confirm", "VerifyEmail", new { userId = user.Id }, Request.Scheme)!;
        await verifyEmailService.SendEmailConfirmation(user, url);
        
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
}