using Microsoft.AspNetCore.Mvc;
using InertiaCore;

namespace Eaze.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}