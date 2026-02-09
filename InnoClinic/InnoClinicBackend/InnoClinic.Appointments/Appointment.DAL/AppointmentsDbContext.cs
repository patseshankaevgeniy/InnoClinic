using Appointment.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DAL;

public class AppointmentsDbContext : DbContext
{
    public DbSet<AppointmentEntity> Appointments { get; set; }
    public DbSet<AppointmentResultEntity> AppointmentResults { get; set; }

    public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentsDbContext).Assembly);

        modelBuilder.Entity<AppointmentEntity>()
            .HasIndex(a => new { a.DoctorId, a.AppointmentDate });
    }
}
