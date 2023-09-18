using Microsoft.EntityFrameworkCore;

namespace Eaze.Infrastructure.Database;

public static class DataServiceExtensions
{
    public static WebApplicationBuilder AddDataServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

            if (builder.Environment.IsDevelopment())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });
        
        return builder;
    }
}