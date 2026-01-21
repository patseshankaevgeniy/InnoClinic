using Appointment.BLL.Models.AppointmentResults;

namespace Appointment.BLL.Services.Interfaces;

public interface IAppointmentResultsService
{
    Task<AppointmentResultModel> GetById(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<AppointmentResultModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<AppointmentResultModel>> GetByDateAsync(DateTime filterDate, CancellationToken cancellationToken, bool isDescending = false);
    Task<AppointmentResultModel> CreateAsync(CreatedAppointmentResultModel createdModel, CancellationToken cancellationToken);
    Task<AppointmentResultModel> UpdateAsync(UpdatedAppointmentResultModel updatedModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
