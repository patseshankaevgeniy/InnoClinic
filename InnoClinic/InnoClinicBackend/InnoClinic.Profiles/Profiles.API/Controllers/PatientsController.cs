using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Common.Constants;
using Profiles.API.Dtos.Patients;
using Profiles.BLL.Models.Patients;
using Profiles.BLL.Services.Interfaces;

namespace Profiles.API.Controllers;

[ApiController]
[Route(RouteConstants.PatientsControllerRoute)]
public class PatientsController(IPatientService patientsService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteConstants.CreateRoute)]
    public async Task<PatientDto> CreateAsync(PatientDto createdPatientDto, CancellationToken cancellationToken = default)
    {
        var createdModel = await patientsService.CreateAsync(mapper.Map<PatientModel>(createdPatientDto), cancellationToken);
        return mapper.Map<PatientDto>(createdModel);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<PatientDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var patientModel = await patientsService.GetByIdAsync(id, cancellationToken);
        return mapper.Map<PatientDto>(patientModel);
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<PatientDto> UpdateAsync(PatientDto updatedPatientDto, CancellationToken cancellationToken = default)
    {
        var updatedModel = await patientsService.UpdateAsync(mapper.Map<PatientModel>(updatedPatientDto), cancellationToken);
        return mapper.Map<PatientDto>(updatedModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await patientsService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
