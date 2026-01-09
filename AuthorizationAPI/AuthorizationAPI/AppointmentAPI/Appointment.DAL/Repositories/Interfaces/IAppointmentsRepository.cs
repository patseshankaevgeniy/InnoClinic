using Appointment.DAL.Entities;

namespace Appointment.DAL.Repositories.Interfaces;

public interface IAppointmentsRepository : IGenericRepository<AppointmentEntity>
{
    Task<IEnumerable<AppointmentEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = true);
    Task<IEnumerable<AppointmentEntity>> GetByDateAsync(DateTime filterDate, bool isDescending, CancellationToken cancellationToken, bool asNoTracking = true);
}
