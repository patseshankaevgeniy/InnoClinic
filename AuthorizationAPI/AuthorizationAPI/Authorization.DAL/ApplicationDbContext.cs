using Aithorization.DAL.Entities;
using Aithorization.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aithorization.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        if (Database.IsRelational())
            Database.Migrate();
        else
            Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

