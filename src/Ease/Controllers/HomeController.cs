using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}