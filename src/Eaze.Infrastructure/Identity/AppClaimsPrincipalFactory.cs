using System.Security.Claims;
using Eaze.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Eaze.Infrastructure.Identity;

public sealed class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
{
    public AppClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        List<Claim> claims = new();

        if (user.Name is not null)
        {
            claims.Add(new Claim(ClaimTypes.GivenName, user.Name));
        }

        foreach (string role in await UserManager.GetRolesAsync(user))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        identity.AddClaims(claims);

        return identity;
    }
}