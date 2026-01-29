using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Common.Constants;
using Profiles.API.Dtos;
using Profiles.API.Dtos.Doctors;
using Profiles.BLL.Models;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Services.Interfaces;

namespace Profiles.API.Controllers;

[ApiController]
[Route(RouteConstants.DoctorsControllerRoute)]
public class DoctorsController(IDoctorsService doctorService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteConstants.CreateRoute)]
    public async Task<DoctorDto> CreateAsync(CreatedDoctorDto newDoctorDto, CancellationToken cancellationToken = default)
    {
        var createdDoctorModel = await doctorService.CreateAsync(mapper.Map<CreatedDoctorModel>(newDoctorDto), cancellationToken);
        return mapper.Map<DoctorDto>(createdDoctorModel);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<DoctorDto> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctorModel = await doctorService.GetByIdAsync(id, cancellationToken);
        return mapper.Map<DoctorDto>(doctorModel);
    }

    [HttpGet(RouteConstants.GetAllRoute)]
    public async Task<List<DoctorDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var doctorModels = await doctorService.GetAllAsync(cancellationToken);
        return mapper.Map<List<DoctorDto>>(doctorModels).ToList();
    }

    [HttpGet(RouteConstants.GetPagedRoute)]
    public async Task<List<DoctorDto>> GetPagedAsync([FromQuery] PaginationParametersDto paginationParameters, CancellationToken cancellationToken)
    {
        var doctors = await doctorService.GetPagedAsync(mapper.Map<PaginationParametersModel>(paginationParameters), cancellationToken);

        return mapper.Map<List<DoctorDto>>(doctors).ToList();
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<DoctorDto> UpdateAsync(UpdatedDoctorDto doctorDto, CancellationToken cancellationToken)
    {
        var updatedDoctorModel = await doctorService.UpdateAsync(mapper.Map<UpdatedDoctorModel>(doctorDto), cancellationToken);
        return mapper.Map<DoctorDto>(updatedDoctorModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await doctorService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
