using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Offices.API.Common;
using Offices.API.Dtos;
using Offices.API.Dtos.Office;
using Offices.BLL.Models;
using Offices.BLL.Services.Interfaces;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Offices.API.Controllers;

[ApiController]
[Route(RouteCostants.OfficesControllerRoute)]
public class OfficesController(IOfficesService officesService, IMapper mapper) : ControllerBase
{
    [HttpGet(RouteCostants.GetAllOfficesRoute)]
    [ProducesResponseType(typeof(OfficeDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<IEnumerable<OfficeDto>> GetAllAsync()
    {
        var offices = await officesService.GetAllAsync();
        return offices.Select(mapper.Map<OfficeDto>).ToList();
    }

    [HttpGet(RouteCostants.GetOfficeRoute)]
    [ProducesResponseType(typeof(OfficeDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<OfficeDto> GetAsync(Guid id)
    {
        var office = await officesService.GetAsync(id);
        return mapper.Map<OfficeDto>(office);
    }

    [HttpPut(RouteCostants.UpdateOfficeRoute)]
    [ProducesResponseType(typeof(OfficeDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<OfficeDto> UpdateAsync(UpdatedOfficeDto updateOfficeDto)
    {
        var updatedOffice = await officesService.UpdateAsync(mapper.Map<OfficeInputModel>(updateOfficeDto));
        return mapper.Map<OfficeDto>(updatedOffice);
    }

    [HttpPost(RouteCostants.CreateOfficeRoute)]
    [ProducesResponseType(typeof(OfficeDto), Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<OfficeDto> CreateAsync(CreatedOfficeDto createdOfficeDto)
    {
        var createdOffice = await officesService.CreateAsync(mapper.Map<OfficeInputModel>(createdOfficeDto));
        return mapper.Map<OfficeDto>(createdOffice);
    }

    [HttpDelete(RouteCostants.DeleteOfficeRoute)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await officesService.DeleteAsync(id);
        return NoContent();
    }
}
