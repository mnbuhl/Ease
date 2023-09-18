using Ease.App.Common.Interfaces;
using Ease.App.Models;
using Ease.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace Ease.Infrastructure.Identity;

public static class IdentityServiceExtensions
{
    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequiredLength = 8,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequiredUniqueChars = 0,
                    RequireNonAlphanumeric = false,
                };
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/auth/login";
            options.LogoutPath = "/auth/logout";
        });

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IVerifyEmailService, VerifyEmailService>();
        builder.Services.AddScoped<IPasswordService, PasswordService>();

        return builder;
    }
}