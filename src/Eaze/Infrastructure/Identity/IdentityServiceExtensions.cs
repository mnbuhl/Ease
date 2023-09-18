using Eaze.App.Common.Interfaces;
using Eaze.App.Models;
using Eaze.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace Eaze.Infrastructure.Identity;

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