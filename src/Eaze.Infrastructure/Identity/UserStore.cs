using System.Security.Claims;
using Eaze.Domain.Models;
using Eaze.Domain.Models.Authorization;
using Eaze.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eaze.Infrastructure.Identity;

public sealed class UserStore(AppDbContext context, ILogger<UserStore> logger) : IUserRoleStore<User>,
    IUserClaimStore<User>
{
    public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var claims = await context.Claims.Where(c => c.UserId == user.Id).ToListAsync(cancellationToken);

        return claims.Select(c => new Claim(c.Type, c.Value)).ToList();
    }

    public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userClaims = claims.Select(c => new UserClaim
        {
            UserId = user.Id,
            Type = c.Type,
            Value = c.Value
        });


        await context.Claims.AddRangeAsync(userClaims, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userClaim = await context.Claims.FirstOrDefaultAsync(c => c.UserId == user.Id && c.Type == claim.Type,
            cancellationToken);

        if (userClaim is null)
        {
            throw new ArgumentException("Invalid claim", nameof(claim));
        }

        userClaim.Type = newClaim.Type;
        userClaim.Value = newClaim.Value;

        context.Claims.Update(userClaim);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userClaims = await context.Claims.Where(c => c.UserId == user.Id).ToListAsync(cancellationToken);

        foreach (var claim in claims)
        {
            var userClaim = userClaims.FirstOrDefault(c => c.Type == claim.Type && c.Value == claim.Value);

            if (userClaim is null)
            {
                throw new ArgumentException("Invalid claim", nameof(claim));
            }

            context.Claims.Remove(userClaim);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userClaims = await context.Claims.Where(c => c.Type == claim.Type && c.Value == claim.Value)
            .ToListAsync(cancellationToken);

        var userIds = userClaims.Select(c => c.UserId);

        return await context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync(cancellationToken);
    }

    public void Dispose()
    {
        // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
        GC.SuppressFinalize(this);
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(user.Username);
    }

    public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        user.Username = userName;

        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(user.NormalizedUsername);
    }

    public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        user.NormalizedUsername = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to create user");
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            context.Entry(user).State = EntityState.Modified;
            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to update user");
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);

            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to delete user");
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var isValidGuid = Guid.TryParse(userId, out var guid);

        if (!isValidGuid)
        {
            throw new ArgumentException("Invalid GUID", nameof(userId));
        }

        return await context.Users.FindAsync(new object[] { guid }, cancellationToken);
    }

    public Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return context.Users.FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUserName, cancellationToken);
    }

    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpperInvariant(),
            cancellationToken);

        if (role is null)
        {
            throw new ArgumentException("Invalid role", nameof(roleName));
        }

        user.Roles.Add(role);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpperInvariant(),
            cancellationToken);

        if (role is null)
        {
            throw new ArgumentException("Invalid role", nameof(roleName));
        }

        user.Roles.Remove(role);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var roles = await context.Roles.Where(r => user.Roles.Contains(r)).ToListAsync(cancellationToken);

        return roles.Select(r => r.Name).ToList();
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpperInvariant(),
            cancellationToken);

        if (role is null)
        {
            throw new ArgumentException("Invalid role", nameof(roleName));
        }

        return user.Roles.Contains(role);
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpperInvariant(),
            cancellationToken);

        if (role is null)
        {
            throw new ArgumentException("Invalid role", nameof(roleName));
        }

        return await context.Users.Where(u => u.Roles.Contains(role)).ToListAsync(cancellationToken);
    }
}