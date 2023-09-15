using Eaze.Application.Common.Interfaces;
using Eaze.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Eaze.Infrastructure.Identity;

public static class IdentityServiceExtensions
{
    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication()
            .AddCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
            });

        builder.Services.AddAuthorizationBuilder();
        
        builder.Services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddRoleStore<RoleStore>()
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddUserStore<UserStore>()
            .AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        
        return builder;
    }
}