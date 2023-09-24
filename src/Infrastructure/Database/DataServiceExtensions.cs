using Microsoft.EntityFrameworkCore;

namespace Ease.Infrastructure.Database;

public static class DataServiceExtensions
{
    public static WebApplicationBuilder AddDataServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
#if (UsePostgreSQL)
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgresConnection"));
#elif (UseMSSQL)
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlServerConnection"));
#else
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultSqliteConnection"));
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