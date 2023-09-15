using InertiaCore;

namespace Eaze.Web.Middleware;

public sealed class InertiaMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sharedData = new Dictionary<string, object?>();

        sharedData.TryAdd("auth", new { Id = Guid.NewGuid(), Email = "test@test.com" });

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
