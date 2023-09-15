using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

public sealed class AuthController : Controller
{
    public IActionResult Login()
    {
        return Inertia.Render("Auth/Login");
    }
}