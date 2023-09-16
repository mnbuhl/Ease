using Eaze.Application.Common.Interfaces;
using Eaze.Application.Features.Auth;
using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

public sealed class AuthController(IAuthService authService) : Controller
{
    public IActionResult Login()
    {
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
        return Inertia.Render("Auth/Register");
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Register();
        }

        await authService.Register(request);

        return RedirectToAction("Index", "Dashboard");
    }
}