using InertiaCore;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Web.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return Inertia.Render("Index");
    }
}