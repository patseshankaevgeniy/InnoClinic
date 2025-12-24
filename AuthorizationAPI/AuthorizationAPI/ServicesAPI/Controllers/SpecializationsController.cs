using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Api.Common.Constants;
using Services.Api.Dtos.Specializations;
using Services.BLL.Models.Specializations;
using Services.BLL.Services.Interfaces;

namespace Services.Api.Controllers;

[ApiController]
[Route(RouteConstants.SpecializationsControllerRoute)]
public class SpecializationsController(ISpecializationsService specializationsService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteConstants.CreateRoute)]
    public async Task<SpecializationDto> CreateAsync(CreatedSpecializationDto newSpecializationDto)
    {
        var newSpecializationModel = await specializationsService.CreateAsync(mapper.Map<CreatedSpecializationModel>(newSpecializationDto));
        return mapper.Map<SpecializationDto>(newSpecializationModel);
    }

    [HttpGet(RouteConstants.GetAllRoute)]
    public async Task<List<SpecializationDto>> GetAllAsync()
    {
        var specializationModels = await specializationsService.GetAllAsync();
        return specializationModels.Select(model => mapper.Map<SpecializationDto>(model)).ToList();
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<SpecializationDto> GetAsync(Guid id)
    {
        var specializationModel = await specializationsService.GetAsync(id);
        return mapper.Map<SpecializationDto>(specializationModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await specializationsService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<SpecializationDto> UpdateAsync(UpdatedSpecializationDto updatedSpecializationDto)
    {
        var updatedSpecializationModel = await specializationsService.UpdateAsync(mapper.Map<UpdatedSpecializationModel>(updatedSpecializationDto));
        return mapper.Map<SpecializationDto>(updatedSpecializationModel);
    }
}
