using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Authorization.API.Controllers;

[Authorize]
[ApiController]
[Route(RouteCostants.IdentityControllerRoute)]
public class IdentityController(IIdentityService identityService) : ControllerBase
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
        var identityModel = await identityService.CreateAsync(new IdentityModel
        {
            Email = newIdentityDto.Email,
            FirstName = newIdentityDto.FirstName,
            LastName = newIdentityDto.LastName,
            Password = newIdentityDto.Password,
            Role = newIdentityDto.Role,
        });

        return new IdentityDto
        {
            FirstName = identityModel.FirstName,
            Email = identityModel.Email,
            LastName = identityModel.LastName,
            Role = identityModel.Role,
        };
    }

    [HttpGet(RouteCostants.GetIdentityRoute)]
    [ProducesResponseType(typeof(IdentityDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<IdentityDto> GetAsync(Guid id)
    {
        var identityModel = await identityService.GetAsync(id);

        return new IdentityDto
        {
            FirstName = identityModel.FirstName,
            LastName = identityModel.LastName,
            Email = identityModel.Email,
            Role = identityModel.Role,
        };
    }

    [HttpPut(RouteCostants.UpdateIdentityRoute)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<CreatedIdentityDto> UpdateAsync(CreatedIdentityDto updatedIdentityDto)
    {
        var identityModel = await identityService.UpdateAsync(new IdentityModel
        {
            Id = updatedIdentityDto.Id,
            Email = updatedIdentityDto.Email,
            FirstName = updatedIdentityDto.FirstName,
            LastName = updatedIdentityDto.LastName,
            Password = updatedIdentityDto.Password,
            Role = updatedIdentityDto.Role,
        });

        updatedIdentityDto.Role = identityModel.Role;
        updatedIdentityDto.Email = identityModel.Email;
        updatedIdentityDto.FirstName = identityModel.FirstName;
        updatedIdentityDto.LastName = identityModel.LastName;
        updatedIdentityDto.Password = identityModel.Password;
        updatedIdentityDto.Id = identityModel.Id;

        return updatedIdentityDto;
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
