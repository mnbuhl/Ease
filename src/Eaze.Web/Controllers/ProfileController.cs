using Eaze.Application.Requests;
using Eaze.Domain.Models;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

[Authorize]
public sealed class ProfileController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
{
    public IActionResult Edit()
    {
        return Inertia.Render("Profile/Edit");
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateProfileRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Edit();
        }

        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        user.Name = request.Name;

        if (user.Email != request.Email)
        {
            var emailExists = await userManager.FindByEmailAsync(request.Email) != null;

            if (emailExists)
            {
                ModelState.AddModelError("email", "Email already exists");
                return Edit();
            }

            user.Email = request.Email;
            user.EmailConfirmed = false;

            // TODO: send confirmation email
        }

        await userManager.UpdateAsync(user);
        await signInManager.SignInAsync(user, true);
        
        return RedirectToAction("Edit");
    }
}