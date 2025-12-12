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
    public async Task<DoctorDto> CreateAsync(CreatedDoctorDto newDoctorDto)
    {
        var createdDoctorModel = await doctorService.CreateAsync(mapper.Map<CreatedDoctorModel>(newDoctorDto));
        return mapper.Map<DoctorDto>(createdDoctorModel);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<DoctorDto> GetAsync(Guid id)
    {
        var doctorModel = await doctorService.GetByIdAsync(id);
        return mapper.Map<DoctorDto>(doctorModel);
    }

    [HttpGet(RouteConstants.GetAllRoute)]
    public async Task<List<DoctorDto>> GetAllAsync()
    {
        var doctorModels = await doctorService.GetAllAsync();
        return mapper.Map<List<DoctorDto>>(doctorModels).ToList();
    }

    [HttpGet(RouteConstants.GetCountRoute)]
    public async Task<List<DoctorDto>> GetCountAsync([FromQuery] PaginationParametersDto paginationParameters)
    {
        var doctors = await doctorService.GetPagedAsync(mapper.Map<PaginationParametersModel>(paginationParameters));

        return mapper.Map<List<DoctorDto>>(doctors).ToList();
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<DoctorDto> UpdateAsync(UpdatedDoctorDto doctorDto)
    {
        var updatedDoctorModel = await doctorService.UpdateAsync(mapper.Map<UpdatedDoctorModel>(doctorDto));
        return mapper.Map<DoctorDto>(updatedDoctorModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await doctorService.DeleteAsync(id);
        return NoContent();
    }
}
