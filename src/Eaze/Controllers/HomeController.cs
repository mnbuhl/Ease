using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}