using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Api.Common.Constants;
using Services.Api.Dtos.Procedures;
using Services.Api.Dtos.Specializations;
using Services.BLL.Models.Procedures;
using Services.BLL.Services.Interfaces;

namespace Services.Api.Controllers;

[ApiController]
[Route(RouteConstants.ProceduresControllerRoute)]
public class ProceduresController(IProceduresService proceduresService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteConstants.CreateRoute)]
    public async Task<ProcedureDto> CreateAsync(CreatedProcedureDto newProcedureDto)
    {
        var newProcedureModel = await proceduresService.CreateAsync(mapper.Map<CreatedProcedureModel>(newProcedureDto));

        return mapper.Map<ProcedureDto>(newProcedureModel);
    }

    [HttpGet(RouteConstants.GetAllRoute)]
    public async Task<List<ProcedureDto>> GetAllAsync()
    {
        var procedureModels = await proceduresService.GetAllAsync();
        return mapper.Map<List<ProcedureDto>>(procedureModels);
    }

    [HttpGet(RouteConstants.GetRoute)]
    public async Task<ProcedureDto> GetAsync(Guid id)
    {
        var procedureModel = await proceduresService.GetAsync(id);
        return mapper.Map<ProcedureDto>(procedureModel);
    }

    [HttpPut(RouteConstants.UpdateRoute)]
    public async Task<ProcedureDto> UpdateAsync(UpdatedSpecializationDto updatedProcedureDto)
    {
        var updatedProcedureModel = await proceduresService.UpdateAsync(mapper.Map<UpdatedProcedureModel>(updatedProcedureDto));
        return mapper.Map<ProcedureDto>(updatedProcedureModel);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await proceduresService.DeleteAsync(id);
        return NoContent();
    }
}
