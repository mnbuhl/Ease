using Eaze.Application.Requests;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

[Authorize]
public sealed class ProfileController : Controller
{
    public IActionResult Edit()
    {
        return Inertia.Render("Profile/Edit");
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateProfileRequest request)
    {
        // TODO: implement functionality
        return RedirectToAction("Edit");
    }
}