using Appointment.DAL.Entities;
using Appointment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DAL.Repositories;

public class AppointmentsRepository(AppointmentsDbContext db) : GenericRepository<AppointmentEntity>(db), IAppointmentsRepository
{
    private readonly DbSet<AppointmentEntity> appointments = db.Set<AppointmentEntity>();

    public async Task<IEnumerable<AppointmentEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = true)
    {
        return await GetQuery(asNoTracking).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByDateAsync(DateTime filterDate, bool isDescending, CancellationToken cancellationToken, bool asNoTracking = true)
    {
        var query = GetQuery(asNoTracking)
                       .Where(a => a.AppointmentDate.Date == filterDate.Date);

        if (isDescending)
        {
            query = query.OrderByDescending(a => a.AppointmentDate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    private IQueryable<AppointmentEntity> GetQuery(bool asNoTracking) =>
       asNoTracking ? appointments.AsNoTracking() : appointments;
}
