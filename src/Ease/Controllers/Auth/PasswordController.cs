using Ease.App.Common.Interfaces;
using Ease.App.Requests;
using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Controllers.Auth;

public sealed class PasswordController(IPasswordService passwordService) : BaseController
{
    public IActionResult Forgot()
    {
        var status = TempData["Status"]?.ToString();

        return Inertia.Render("Auth/ForgotPassword",
            new { Status = status, CanResetPassword = string.IsNullOrEmpty(status) });
    }

    [HttpPost]
    public async Task<IActionResult> Forgot([FromBody] ForgotPasswordRequest request)
    {
        string url = Url.Action("Reset", "Password", new { request.Email }, Request.Scheme)!;

        await passwordService.SendPasswordReset(request.Email, url);

        return Back(new { Status = "Password reset link sent" });
    }

    public IActionResult Reset(string email, string token)
    {
        return Inertia.Render("Auth/ResetPassword", new { Email = email, Token = token });
    }

    [HttpPost]
    public async Task<IActionResult> Reset([FromBody] ResetPasswordRequest request)
    {
        var (email, token, password, _) = request;

        if (!ModelState.IsValid)
        {
            return Reset(email, token);
        }

        await passwordService.ResetPassword(email, token, password);

        bool isAuthenticated = User.Identity?.IsAuthenticated ?? false;

        if (isAuthenticated)
        {
            return RedirectToAction("Edit", "Profile");
        }

        return RedirectToAction("Login", "Auth");
    }
}