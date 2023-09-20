using Microsoft.AspNetCore.Mvc;

namespace Ease.Web.Controllers;

public abstract class BaseController : Controller
{
    public IActionResult Back(object? data = null)
    {
        string referer = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(referer))
        {
            return RedirectToAction("Index", "Home");
        }

        if (data is not null)
        {
            foreach (var prop in data.GetType().GetProperties())
            {
                TempData[prop.Name] = prop.GetValue(data);
            }
        }

        return Redirect(referer);
    }
}