using Appointment.DAL.Entities;

namespace Appointment.DAL.Repositories.Interfaces;

public interface IAppointmentResultsRepository : IGenericRepository<AppointmentResultEntity>
{
    Task<List<AppointmentResultEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<List<AppointmentResultEntity>> GetByDateAsync(DateTime filterDate, bool isDescending = false, bool asNoTracking = true, CancellationToken cancellationToken = default);
}
