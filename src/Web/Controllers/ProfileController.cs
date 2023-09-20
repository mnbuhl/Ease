using Ease.App.Common.Interfaces;
using Ease.App.Models;
using Ease.Web.Requests;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ease.Web.Controllers;

[Authorize]
public sealed class ProfileController(UserManager<User> userManager, SignInManager<User> signInManager,
    IVerifyEmailService verifyEmailService, IPasswordService passwordService) : BaseController
{
    public IActionResult Edit()
    {
        return Inertia.Render("Profile/Edit", new { MustVerifyEmail = true, Status = TempData["Status"] });
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

    [HttpPatch]
    public async Task<IActionResult> Password([FromBody] NewPasswordRequest request)
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

        var result = await signInManager.CheckPasswordSignInAsync(user, request.CurrentPassword, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("currentPassword", "Current password is incorrect");
            return Edit();
        }

        await passwordService.ChangePassword(user, request.CurrentPassword, request.Password);

        return Back(new { Status = "Password Changed" });
    }
}