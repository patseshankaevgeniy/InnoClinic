using Appointment.DAL.Entities;

namespace Appointment.DAL.Repositories.Interfaces;

public interface IAppointmentsRepository : IGenericRepository<AppointmentEntity>
{
    Task<List<AppointmentEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<List<AppointmentEntity>> GetByDateAsync(DateTime filterStartDate, bool isDescending, bool asNoTracking = true, CancellationToken cancellationToken = default);
}
