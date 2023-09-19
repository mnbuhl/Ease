using Ease.App;
using Ease.Infrastructure.Database;
using Ease.Infrastructure.Identity;
using Ease.Infrastructure.Mailing;
using InertiaCore.Extensions;

namespace Ease;

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