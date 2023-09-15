namespace Eaze.Web;

public static class AppServices
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
    }
}