using System.Security.Claims;
using Ease.App.Common.Interfaces;
using Ease.App.Models;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Controllers.Auth;

[Route("verify-email/{action=Index}")]
public sealed class VerifyEmailController(IVerifyEmailService verifyEmailService, UserManager<User> userManager)
    : BaseController
{
    [Authorize]
    public IActionResult Index()
    {
        var status = HttpContext.Session.GetString("Status");

        return Inertia.Render("Auth/VerifyEmail",
            new { Status = status, CanVerifyEmail = string.IsNullOrEmpty(status) });
    }

    public async Task<IActionResult> Confirm(Guid userId, string token)
    {
        await verifyEmailService.ConfirmEmail(userId, token);

        TempData["Status"] = "email-confirmed";

        bool isAuthenticated = User.Identity?.IsAuthenticated == true;

        return RedirectToAction("Index", isAuthenticated ? "Dashboard" : "Home");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Resend()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return NotFound();
        }

        string url = Url.Action("Confirm", "VerifyEmail", new { userId = user.Id }, Request.Scheme)!;

        await verifyEmailService.SendEmailConfirmation(user, url);

        HttpContext.Session.SetString("Status", "verification-link-sent");

        return Back();
    }
}