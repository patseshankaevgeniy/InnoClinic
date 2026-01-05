using Appointment.DAL.Entities;
using Appointment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DAL.Repositories;

public class AppointmentsRepository(AppointmentsDbContext db) : GenericRepository<AppointmentEntity>(db), IAppointmentsRepository
{
    private readonly DbSet<AppointmentEntity> appointments = db.Set<AppointmentEntity>();

    public Task<List<AppointmentEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return GetQuery(asNoTracking).ToListAsync(cancellationToken);
    }

    public Task<List<AppointmentEntity>> GetByDateAsync(DateTime filterDate, bool isDescending, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = GetQuery(asNoTracking)
                        .Where(a => a.AppointmentDate >= filterDate)
                        .OrderByDescending(a => a.AppointmentDate);

        return query.ToListAsync(cancellationToken);
    }

    private IQueryable<AppointmentEntity> GetQuery(bool asNoTracking) =>
       asNoTracking ? appointments.AsNoTracking() : appointments;
}
