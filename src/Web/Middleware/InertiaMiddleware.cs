using System.Security.Claims;
using Ease.App.Constants;
using InertiaCore;

namespace Ease.Web.Middleware;

public sealed class InertiaMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sharedData = new Dictionary<string, object?>();

        if (context.User.Identity?.IsAuthenticated == true)
        {
            sharedData.TryAdd("auth", new Dictionary<string, object?>
            {
                {
                    "user", new
                    {
                        Id = context.User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Email = context.User.FindFirstValue(ClaimTypes.Email),
                        Name = context.User.FindFirstValue(ClaimTypes.GivenName),
                        Roles = context.User.FindAll(ClaimTypes.Role).Select(x => x.Value),
                        EmailVerified = Convert.ToBoolean(context.User.FindFirstValue(AppClaim.EmailVerified))
                    }
                }
            });
        }

        Inertia.Share(sharedData);

        await next(context);
    }
}

public static class InertiaMiddlewareExtensions
{
    public static IApplicationBuilder UseInertiaSharedData(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<InertiaMiddleware>();
    }
}