using Appointment.BLL.Common.Exceptions;
using Appointment.BLL.Models.Appointments;
using Appointment.BLL.Services.Interfaces;
using Appointment.DAL.Entities;
using Appointment.DAL.Enums;
using Appointment.DAL.Repositories.Interfaces;
using AutoMapper;
using InnoClinic.Contracts.Events;
using MassTransit;

namespace Appointment.BLL.Services;

public class AppointmentsService(
    IAppointmentsRepository appointmentsRepository,
    IPublishEndpoint publishEndpoint,
    IMapper mapper) : IAppointmentsService
{
    public async Task<AppointmentModel> CreateAsync(CreatedAppointmentModel createdModel, CancellationToken cancellationToken = default)
    {
        var createdAppointment = mapper.Map<AppointmentEntity>(createdModel);

        createdAppointment.Status = AppointmentStatus.AwaitingApproval;

        createdAppointment = await appointmentsRepository.CreateAsync(createdAppointment, cancellationToken);

        await publishEndpoint.Publish(new AppointmentCreatedEvent
        {
            AppointmentId = createdAppointment.Id,
            OfficeId = createdModel.OfficeId,
            DoctorId = createdModel.DoctorId,
            DateTime = createdModel.AppointmentDate
        });

        return mapper.Map<AppointmentModel>(createdAppointment);
    }

    public async Task<IEnumerable<AppointmentModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var appointmentEntities = await appointmentsRepository.GetAllAsync(cancellationToken: cancellationToken);

        return mapper.Map<List<AppointmentModel>>(appointmentEntities);
    }

    public async Task<AppointmentModel> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var appointmentEntity = await EnsureExistsAsync(id, cancellationToken: cancellationToken);

        return mapper.Map<AppointmentModel>(appointmentEntity);
    }

    public async Task<IEnumerable<AppointmentModel>> GetByFilteredDateAsync(DateTime filterDate, CancellationToken cancellationToken, bool isDescending = false)
    {
        var filteredAppointments = await appointmentsRepository.GetByDateAsync(filterDate, isDescending, cancellationToken: cancellationToken);

        return mapper.Map<List<AppointmentModel>>(filteredAppointments);
    }

    public async Task<AppointmentModel> UpdateAsync(UpdatedAppointmentModel updatedModel, CancellationToken cancellationToken = default)
    {
        var updatedAppointment = await EnsureExistsAsync(updatedModel.Id, cancellationToken: cancellationToken);

        mapper.Map(updatedModel, updatedAppointment);
        updatedAppointment = await appointmentsRepository.UpdateAsync(updatedAppointment, cancellationToken);

        return mapper.Map<AppointmentModel>(updatedAppointment);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var deletedAppointment = await EnsureExistsAsync(id, cancellationToken: cancellationToken);

        await appointmentsRepository.DeleteAsync(deletedAppointment, cancellationToken);
    }

    private async Task<AppointmentEntity> EnsureExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingAppointment = await appointmentsRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
       
        return existingAppointment ?? throw new NotFoundException(ExceptionMessages.AppointmentNotFound); ;
    }
}
