using Appointment.BLL.Models.Appointments;

namespace Appointment.BLL.Services.Interfaces;

public interface IAppointmentsService
{
    Task<AppointmentModel> GetById(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<AppointmentModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<AppointmentModel>> GetByFilteredDateAsync(DateTime filterStartDate, CancellationToken cancellationToken, bool isDescending = false);
    Task<AppointmentModel> CreateAsync(CreatedAppointmentModel createdModel, CancellationToken cancellationToken);
    Task<AppointmentModel> UpdateAsync(UpdatedAppointmentModel updatedModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
