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
    public async Task<AppointmentDto> CreateAsync(CreatedAppointmentDto newAppointmentDto)
    {
        var newAppointmentModel = await appointmentsService.CreateAsync(mapper.Map<CreatedAppointmentModel>(newAppointmentDto));
        return mapper.Map<AppointmentDto>(newAppointmentModel);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<AppointmentDto> GetByIdAsync(Guid id)
    {
        var appointmentModel = await appointmentsService.GetById(id);
        return mapper.Map<AppointmentDto>(appointmentModel);
    }

    [HttpGet(RouteConstants.GetAllRoute)]
    public async Task<List<AppointmentDto>> GetAllAsync()
    {
        var appointmentModels = await appointmentsService.GetAllAsync();
        return mapper.Map<List<AppointmentDto>>(appointmentModels);
    }

    [HttpGet(RouteConstants.GetFilteredByDateRoute)]
    public async Task<List<AppointmentDto>> GetByFilteredDateAsync(DateTime filterStartDate)
    {
        var appointmentModels = await appointmentsService.GetByFilteredDateAsync(filterStartDate);
        return mapper.Map<List<AppointmentDto>>(appointmentModels);
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<AppointmentDto> UpdateAsync(UpdatedAppointmentDto updatedAppointmentDto)
    {
        var updatedAppointmentModel = await appointmentsService.UpdateAsync(mapper.Map<UpdatedAppointmentModel>(updatedAppointmentDto));
        return mapper.Map<AppointmentDto>(updatedAppointmentModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task DeleteAsync(Guid id)
    {
        await appointmentsService.DeleteAsync(id);
    }
}
