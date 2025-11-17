using Authorization.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Authorization.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Identity> Identities { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Worker> Workers { get; set; }

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
        modelBuilder.Entity<Identity>().UseTptMappingStrategy();
        modelBuilder.Entity<Patient>()
            .Property(e => e.Role)
            .HasConversion(new EnumToStringConverter<UserRole>());
    }
}
