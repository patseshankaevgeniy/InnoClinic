using Microsoft.EntityFrameworkCore;
using Services.DAL.Entities;

namespace Services.DAL;

public class ServicesDbContext : DbContext
{
    private readonly TimeProvider _timeProvider;

    public DbSet<Procedure> Procedures { get; set; }
    public DbSet<Specialization> Specializations { get; set; }

    public ServicesDbContext(DbContextOptions<ServicesDbContext> options, TimeProvider timeProvider) : base(options)
    {
        _timeProvider = timeProvider;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimeStamps();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServicesDbContext).Assembly);
    }

    private void ApplyTimeStamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        var now = _timeProvider.GetUtcNow().DateTime;

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                entity.UpdatedAt = now;
            }

            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
                entry.Property("CreatedAt").IsModified = false;
            }
        }
    }
}
