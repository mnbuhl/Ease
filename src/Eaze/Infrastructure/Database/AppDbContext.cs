using Eaze.App.Models;
using Eaze.Infrastructure.Database.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eaze.Infrastructure.Database;

public sealed class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(new TimestampsInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Remove AspNet prefix from tables
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            string? table = entityType.GetTableName();

            if (table is not null && table.StartsWith("AspNet"))
            {
                entityType.SetTableName(table[6..]);
            }
        }
    }
}