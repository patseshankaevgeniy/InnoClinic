using Appointment.DAL.Entities;
using Appointment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DAL.Repositories;

public class AppointmentResultsRepository(AppointmentsDbContext db) : GenericRepository<AppointmentResultEntity>(db), IAppointmentResultsRepository
{
    private readonly DbSet<AppointmentResultEntity> appointmentResults = db.Set<AppointmentResultEntity>();

    public Task<List<AppointmentResultEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return GetQuery(asNoTracking)
            .ToListAsync(cancellationToken);
    }

    public Task<List<AppointmentResultEntity>> GetByDateAsync(DateTime filterDate, bool isDescending = false, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = GetQuery(asNoTracking)
                        .Where(a => a.ResultDate >= filterDate)
                        .OrderByDescending(a => a.ResultDate);

        return query.ToListAsync(cancellationToken);
    }

    private IQueryable<AppointmentResultEntity> GetQuery(bool asNoTracking) =>
      asNoTracking ? appointmentResults.AsNoTracking() : appointmentResults;
}
