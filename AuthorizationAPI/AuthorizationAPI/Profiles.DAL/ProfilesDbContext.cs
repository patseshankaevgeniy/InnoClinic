using Microsoft.EntityFrameworkCore;
using Profiles.DAL.Entities;

namespace Profiles.DAL
{
    public class ProfilesDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfilesDbContext).Assembly);
        }
    }
}
