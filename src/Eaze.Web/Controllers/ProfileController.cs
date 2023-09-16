using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

[Authorize]
public sealed class ProfileController : Controller
{
    public IActionResult Edit()
    {
        return Inertia.Render("Profile/Edit");
    }
}