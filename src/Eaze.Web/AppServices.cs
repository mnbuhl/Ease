using Eaze.Infrastructure.Data;
using Eaze.Infrastructure.Identity;
using InertiaCore.Extensions;

namespace Eaze.Web;

public static class AppServices
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInertia(options =>
        {
            options.SsrEnabled = false;
        });
        
        builder.Services.AddViteHelper(options =>
        {
            options.PublicDirectory = "wwwroot";
            options.BuildDirectory = "dist";
            options.HotFile = "hot";
            options.ManifestFilename = "manifest.json";
        });
        
        builder.Services.AddControllersWithViews();

        builder.AddDataServices();
        builder.AddIdentityServices();
    }
}