using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Controllers;

[Authorize]
public sealed class DashboardController : BaseController
{
    public IActionResult Index()
    {
        var status = TempData["Status"]?.ToString();

        return Inertia.Render("Dashboard/Index", new { Status = status });
    }
}