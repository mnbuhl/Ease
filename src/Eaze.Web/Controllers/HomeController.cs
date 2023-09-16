using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}