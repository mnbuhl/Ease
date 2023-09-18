using Eaze.App.Common.Interfaces;
using Eaze.App.Models;
using Eaze.App.Requests;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eaze.Controllers;

[Authorize]
public sealed class ProfileController(UserManager<User> userManager, SignInManager<User> signInManager,
    IVerifyEmailService verifyEmailService) : BaseController
{
    public IActionResult Edit()
    {
        return Inertia.Render("Profile/Edit", new { MustVerifyEmail = true });
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
            string url = Url.Action("Confirm", "VerifyEmail", new { userId = user.Id }, Request.Scheme)!;
            await verifyEmailService.SendEmailConfirmation(user, url);
        }
        
        await signInManager.SignInAsync(user, true);

        return Back();
    }
}