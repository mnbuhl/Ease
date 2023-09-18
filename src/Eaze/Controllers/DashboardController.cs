using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Controllers;

[Authorize]
public sealed class DashboardController : BaseController
{
    public IActionResult Index()
    {
        return Inertia.Render("Dashboard/Index");
    }
}