﻿using Microsoft.AspNetCore.Mvc;

namespace Eaze.Controllers;

public abstract class BaseController : Controller
{
    public IActionResult Back()
    {
        string referer = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(referer))
        {
            return RedirectToAction("Index", "Home");
        }

        return Redirect(referer);
    }
}