using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Common.Constants;
using Profiles.API.Dtos.Patients;
using Profiles.API.Dtos.Receptionists;
using Profiles.BLL.Models.Receptionists;
using Profiles.BLL.Services.Interfaces;

namespace Profiles.API.Controllers;

[ApiController]
[Route(RouteConstants.ReceptionistsControllerRoute)]
public class ReceptionistsController(IReceptionistsService receptionistsService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteConstants.CreateRoute)]
    public async Task<ReceptionistDto> CreateAsync(ReceptionistDto createdPatientDto, CancellationToken cancellationToken = default)
    {
        var createdModel = await receptionistsService.CreateAsync(mapper.Map<ReceptionistModel>(createdPatientDto), cancellationToken);
        return mapper.Map<ReceptionistDto>(createdModel);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<ReceptionistDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var patientModel = await receptionistsService.GetByIdAsync(id, cancellationToken);
        return mapper.Map<ReceptionistDto>(patientModel);
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<ReceptionistDto> UpdateAsync(ReceptionistDto updatedPatientDto, CancellationToken cancellationToken = default)
    {
        var updatedModel = await receptionistsService.UpdateAsync(mapper.Map<ReceptionistModel>(updatedPatientDto), cancellationToken);
        return mapper.Map<ReceptionistDto>(updatedModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await receptionistsService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
