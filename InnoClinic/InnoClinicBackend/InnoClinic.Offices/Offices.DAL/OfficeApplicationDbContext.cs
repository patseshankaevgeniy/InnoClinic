using Microsoft.EntityFrameworkCore;
using Offices.DAL.Entities;

namespace Offices.DAL;

public class OfficeApplicationDbContext : DbContext
{
    public DbSet<Office> Offices { get; set; }

    public OfficeApplicationDbContext(DbContextOptions<OfficeApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeApplicationDbContext).Assembly);
    }
}
