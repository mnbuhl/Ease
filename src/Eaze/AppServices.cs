using Eaze.App;
using Eaze.Infrastructure.Database;
using Eaze.Infrastructure.Identity;
using Eaze.Infrastructure.Mailing;
using InertiaCore.Extensions;

namespace Eaze;

public static class AppServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInertia(options => { options.SsrEnabled = false; });

        builder.Services.AddViteHelper(options =>
        {
            options.PublicDirectory = "wwwroot";
            options.BuildDirectory = "dist";
            options.HotFile = "hot";
            options.ManifestFilename = "manifest.json";
        });

        builder.Services.AddControllersWithViews()
            .AddSessionStateTempDataProvider();

        builder.Services.AddSession();

        builder.AddApplicationServices();
        builder.AddDataServices();
        builder.AddIdentityServices();
        builder.AddEmailServices();
    }
}