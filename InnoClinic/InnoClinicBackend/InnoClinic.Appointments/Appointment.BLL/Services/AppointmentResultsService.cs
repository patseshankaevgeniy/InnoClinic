using Appointment.BLL.Models.AppointmentResults;
using Appointment.BLL.Services.Interfaces;
using Appointment.DAL.Entities;
using Appointment.DAL.Repositories.Interfaces;
using AutoMapper;

namespace Appointment.BLL.Services;

public class AppointmentResultsService(IAppointmentResultsRepository repository, IMapper mapper) : IAppointmentResultsService
{
    public async Task<AppointmentResultModel> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken: cancellationToken);

        return mapper.Map<AppointmentResultModel>(entity);
    }

    public async Task<AppointmentResultModel> CreateAsync(CreatedAppointmentResultModel createdModel, CancellationToken cancellationToken)
    {
        var newEntity = await repository.CreateAsync(mapper.Map<AppointmentResultEntity>(createdModel), cancellationToken);

        return mapper.Map<AppointmentResultModel>(newEntity);
    }

    public async Task<IReadOnlyCollection<AppointmentResultModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken: cancellationToken)
            .ContinueWith(task => mapper.Map<List<AppointmentResultModel>>(task.Result), cancellationToken);
    }

    public async Task<IReadOnlyCollection<AppointmentResultModel>> GetByDateAsync(DateTime filterDate, CancellationToken cancellationToken, bool isDescending = false)
    {
        var entities = await repository.GetByDateAsync(filterDate, isDescending, cancellationToken: cancellationToken);

        return mapper.Map<List<AppointmentResultModel>>(entities);
    }

    public async Task<AppointmentResultModel> UpdateAsync(UpdatedAppointmentResultModel updatedModel, CancellationToken cancellationToken)
    {
        var updatedEntity = await repository.UpdateAsync(mapper.Map<AppointmentResultEntity>(updatedModel), cancellationToken);
        return mapper.Map<AppointmentResultModel>(updatedEntity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken: cancellationToken);
        await repository.DeleteAsync(entity, cancellationToken);
    }
}
