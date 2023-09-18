using Ease.App.Constants;
using Microsoft.AspNetCore.Identity;

namespace Ease.Infrastructure.Database.Seeders;

public static class RoleSeeder
{
    public static void Run(AppDbContext context)
    {
        if (context.Roles.Any())
            return;

        context.Roles.AddRange(
            new IdentityRole<Guid>
            {
                Name = Role.User,
                NormalizedName = Role.User.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole<Guid>
            {
                Name = Role.Admin,
                NormalizedName = Role.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        );

        context.SaveChanges();
    }
}