using Eaze.Domain.Models;
using Eaze.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eaze.Infrastructure.Identity;

public sealed class RoleStore(AppDbContext context, ILogger<RoleStore> logger) : IRoleStore<Role>
{
    public void Dispose()
    {
        // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
        GC.SuppressFinalize(this);
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            context.Roles.Add(role);
            await context.SaveChangesAsync(cancellationToken);
            
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to create role");
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            context.Entry(role).State = EntityState.Modified;
            context.Roles.Update(role);
            await context.SaveChangesAsync(cancellationToken);
            
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to update role");
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            context.Roles.Remove(role);
            await context.SaveChangesAsync(cancellationToken);
            
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to delete role");
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return Task.FromResult(role.Id.ToString());
    }

    public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return Task.FromResult(role.Name)!;
    }

    public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        role.Name = roleName!;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return Task.FromResult(role.NormalizedName)!;
    }

    public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        role.NormalizedName = normalizedName!;
        return Task.CompletedTask;
    }

    public async Task<Role?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var isValidGuid = Guid.TryParse(roleId, out var guid);
        
        if (!isValidGuid)
        {
            throw new ArgumentException("Invalid GUID", nameof(roleId));
        }
        
        return await context.Roles.FindAsync(new object[] { guid }, cancellationToken);
    }

    public async Task<Role?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return await context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
    }
}