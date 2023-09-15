using System.Security.Claims;
using Eaze.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Eaze.Infrastructure.Identity;

public sealed class AppClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<User>(userManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };

        if (user.Username is not null)
        {
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
        }

        if (user.Name is not null)
        {
            claims.Add(new Claim(ClaimTypes.GivenName, user.Name));
        }
        
        foreach (string role in await userManager.GetRolesAsync(user))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        identity.AddClaims(claims);
        
        return identity;
    }
}