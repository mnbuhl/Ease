using Eaze.Infrastructure.Data.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Eaze.Infrastructure.Data;

public class DatabaseManager : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<DatabaseManager> _logger;
    private readonly IServiceScope _scope;

    public DatabaseManager(IServiceProvider provider)
    {
        var scope = provider.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseManager>>();
        _scope = scope;
    }

    public void Seed()
    {
        if (_env.IsDevelopment())
        {
            // register development seeders here
        }

        RoleSeeder.Run(_context);

        _logger.LogInformation("Seeding complete");
    }

    public void Migrate()
    {
        _context.Database.Migrate();
    }

    public void Dispose()
    {
        _context.Dispose();
        _scope.Dispose();
    }
}