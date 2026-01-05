using Appointment.BLL.Models.Appointments;

namespace Appointment.BLL.Services.Interfaces;

public interface IAppointmentsService
{
    Task<AppointmentModel> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<List<AppointmentModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<AppointmentModel>> GetByFilteredDateAsync(DateTime filterStartDate, bool isDescending = false, CancellationToken cancellationToken = default);
    Task<AppointmentModel> CreateAsync(CreatedAppointmentModel createdModel, CancellationToken cancellationToken = default);
    Task<AppointmentModel> UpdateAsync(UpdatedAppointmentModel updatedModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
