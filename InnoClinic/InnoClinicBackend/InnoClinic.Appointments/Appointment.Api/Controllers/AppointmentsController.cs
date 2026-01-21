using Appointment.Api.Common;
using Appointment.Api.Dtos.Appointments;
using Appointment.BLL.Models.Appointments;
using Appointment.BLL.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Api.Controllers;

[ApiController]
[Route(RouteConstants.AppointmentsControllerRoute)]
public class AppointmentsController(IAppointmentsService appointmentsService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteConstants.CreateRoute)]
    public async Task<AppointmentDto> CreateAsync(CreatedAppointmentDto newAppointmentDto, CancellationToken cancellationToken)
    {
        var newAppointmentModel = await appointmentsService.CreateAsync(mapper.Map<CreatedAppointmentModel>(newAppointmentDto), cancellationToken);

        return mapper.Map<AppointmentDto>(newAppointmentModel);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<AppointmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var appointmentModel = await appointmentsService.GetById(id, cancellationToken);

        return mapper.Map<AppointmentDto>(appointmentModel);
    }

    [HttpGet(RouteConstants.GetAllRoute)]
    public async Task<IEnumerable<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var appointmentModels = await appointmentsService.GetAllAsync(cancellationToken);

        return mapper.Map<List<AppointmentDto>>(appointmentModels);
    }

    [HttpGet(RouteConstants.GetFilteredByDateRoute)]
    public async Task<IEnumerable<AppointmentDto>> GetByFilteredDateAsync(DateTime filterStartDate, CancellationToken cancellationToken)
    {
        var appointmentModels = await appointmentsService.GetByFilteredDateAsync(filterStartDate, cancellationToken);

        return mapper.Map<List<AppointmentDto>>(appointmentModels);
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<AppointmentDto> UpdateAsync(UpdatedAppointmentDto updatedAppointmentDto, CancellationToken cancellationToken)
    {
        var updatedAppointmentModel = await appointmentsService.UpdateAsync(mapper.Map<UpdatedAppointmentModel>(updatedAppointmentDto), cancellationToken);

        return mapper.Map<AppointmentDto>(updatedAppointmentModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await appointmentsService.DeleteAsync(id, cancellationToken);
    }
}
