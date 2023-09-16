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

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        await authService.Login(loginRequest);

        return Inertia.Render("Index");
    }
}