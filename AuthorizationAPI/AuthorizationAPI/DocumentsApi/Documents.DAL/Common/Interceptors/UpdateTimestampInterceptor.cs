using Documents.DAL.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Documents.DAL.Common.Interceptors;

public class UpdateTimestampInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
         DbContextEventData eventData,
         InterceptionResult<int> result,
         CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = eventData.Context.ChangeTracker
            .Entries<IHasCreatedAt>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            entry.Entity.CreatedAt = timeProvider.GetUtcNow().UtcDateTime;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
