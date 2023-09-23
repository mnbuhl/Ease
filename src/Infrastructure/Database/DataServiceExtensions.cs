using Microsoft.EntityFrameworkCore;

namespace Ease.Infrastructure.Database;

public static class DataServiceExtensions
{
    public static WebApplicationBuilder AddDataServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
#if (UseSQLite)
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultSqliteConnection"));
#elif (UseMSSQL)
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlServerConnection"));
#else
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgresConnection"));
#endif
            
            if (builder.Environment.IsDevelopment())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });
        
        return builder;
    }
}