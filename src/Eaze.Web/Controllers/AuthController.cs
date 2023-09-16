using Eaze.Application.Common.Interfaces;
using Eaze.Application.Requests;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

public sealed class AuthController(IAuthService authService) : Controller
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

        await authService.Register(request);
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