using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Offices.API.Common;
using Offices.API.Dtos;
using Offices.BLL.Models;
using Offices.BLL.Services.Interfaces;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Offices.API.Controllers;

[ApiController]
[Route(RouteCostants.OfficesControllerRoute)]
public class OfficesController(IOfficesService officesService, IMapper mapper) : ControllerBase
{
    [HttpGet(RouteCostants.GetAllOfficesRoute)]
    [ProducesResponseType(typeof(OfficeResourceDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<IEnumerable<OfficeResourceDto>> GetAllAsync()
    {
        var offices = await officesService.GetAllAsync();
        return offices.Select(mapper.Map<OfficeResourceDto>).ToList();
    }

    [HttpGet(RouteCostants.GetOfficeRoute)]
    [ProducesResponseType(typeof(OfficeResourceDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<OfficeResourceDto> GetAsync(Guid id)
    {
        var office = await officesService.GetAsync(id);
        return mapper.Map<OfficeResourceDto>(office);
    }

    [HttpPut(RouteCostants.UpdateOfficeRoute)]
    [ProducesResponseType(typeof(OfficeInputDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<OfficeInputDto> UpdateAsync(OfficeInputDto updateOfficeDto)
    {
        var updatedOffice = await officesService.UpdateAsync(mapper.Map<OfficeInputModel>(updateOfficeDto));
        return mapper.Map<OfficeInputDto>(updatedOffice);
    }

    [HttpPost(RouteCostants.CreateOfficeRoute)]
    [ProducesResponseType(typeof(OfficeInputDto), Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<OfficeInputDto> CreateAsync(OfficeInputDto newOfficeDto)
    {
        var createdOffice = await officesService.CreateAsync(mapper.Map<OfficeInputModel>(newOfficeDto));
        return mapper.Map<OfficeInputDto>(createdOffice);
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
