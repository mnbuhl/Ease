using Ease.App.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ease.Infrastructure.Database.Interceptors;

public sealed class TimestampsInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        HandleChanges(eventData);
        return base.SavingChanges(eventData, result);
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new())
    {
        HandleChanges(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void HandleChanges(DbContextEventData eventData)
    {
        if (eventData.Context is null)
        {
            return;
        }
        
        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry.Entity is not ITimestamped entity) continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.Created = DateTime.UtcNow;
                    entity.Updated = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.Updated = DateTime.UtcNow;
                    break;
            }
        }
    }
}
