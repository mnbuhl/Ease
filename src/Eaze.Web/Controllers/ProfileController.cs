using Eaze.Application.Common.Interfaces;
using Eaze.Application.Requests;
using Eaze.Domain.Models;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Web.Controllers;

[Authorize]
public sealed class ProfileController(UserManager<User> userManager, SignInManager<User> signInManager,
    IAuthService authService) : Controller
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

        bool emailChanged = false;

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

            emailChanged = true;
        }

        await userManager.UpdateAsync(user);

        if (emailChanged)
        {
            string url = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id }, Request.Scheme)!;
            await authService.GenerateAndSendEmailConfirmationToken(user, url);
        }
        
        await signInManager.SignInAsync(user, true);
        
        return RedirectToAction("Edit");
    }
}