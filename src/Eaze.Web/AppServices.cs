using InertiaCore.Extensions;

namespace Eaze.Web;

public static class AppServices
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInertia(x =>
        {
            x.SsrEnabled = false;
        });
        
        builder.Services.AddViteHelper(options =>
        {
            options.PublicDirectory = "wwwroot";
            options.BuildDirectory = "dist";
        });
        
        builder.Services.AddControllersWithViews();
    }
}