using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Controllers;

[Authorize]
public sealed class DashboardController : BaseController
{
    public IActionResult Index()
    {
        return Inertia.Render("Dashboard/Index");
    }
}