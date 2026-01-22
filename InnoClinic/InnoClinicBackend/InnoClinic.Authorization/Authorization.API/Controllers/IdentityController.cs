using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Authorization.API.Controllers;

[Authorize]
[ApiController]
[Route(RouteCostants.IdentityControllerRoute)]
public class IdentityController(IIdentityService identityService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteCostants.CreateIdentityRoute)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IdentityDto), Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<ActionResult<IdentityDto>> CreateAsync(CreatedIdentityDto newIdentityDto)
    {
        var identityModel = await identityService.CreateAsync(mapper.Map<IdentityModel>(newIdentityDto));

        return mapper.Map<IdentityDto>(identityModel);
    }

    [HttpGet(RouteCostants.GetIdentityRoute)]
    [ProducesResponseType(typeof(IdentityDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<IdentityDto> GetAsync(Guid id)
    {
        var identityModel = await identityService.GetAsync(id);

        return mapper.Map<IdentityDto>(identityModel);
    }

    [HttpPut(RouteCostants.UpdateIdentityRoute)]
    [ProducesResponseType(typeof(IdentityDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<IdentityDto> UpdateAsync(CreatedIdentityDto updatedIdentityDto)
    {
        var identityModel = await identityService.UpdateAsync(mapper.Map<IdentityModel>(updatedIdentityDto));

        return mapper.Map<IdentityDto>(identityModel);
    }

    [HttpDelete(RouteCostants.DeleteIdentityRoute)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await identityService.DeleteAsync(id);
        return NoContent();
    }
}
