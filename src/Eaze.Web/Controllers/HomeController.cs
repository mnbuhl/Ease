using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}