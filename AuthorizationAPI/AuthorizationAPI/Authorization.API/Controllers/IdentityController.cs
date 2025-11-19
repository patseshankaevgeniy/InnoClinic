using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Authorization.API.Controllers;

[ApiController]
[Route(RouteCostants.IdentityRoute)]
[Authorize]
public class IdentityController(IdentityService identityService) : ControllerBase
{
    [HttpPost(RouteCostants.CreateIdentityRoute)]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(Status201Created, Type = typeof(IdentityDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status403Forbidden, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IdentityDto>> CreateIdentityAsync(IdentityDto newIdentityDto)
    {
        var identityModel = await identityService.CreateAsync(new IdentityModel
        {
            Email = newIdentityDto.Email,
            FirstName = newIdentityDto.FirstName,
            LastName = newIdentityDto.LastName,
            HashPassword = newIdentityDto.HashPassword,
            Role = newIdentityDto.Role,
        });

        newIdentityDto.Role = identityModel.Role;
        newIdentityDto.Email = identityModel.Email;
        newIdentityDto.FirstName = identityModel.FirstName;
        newIdentityDto.LastName = identityModel.LastName;
        newIdentityDto.HashPassword = identityModel.HashPassword;
        newIdentityDto.Id = identityModel.Id;

        return newIdentityDto;
    }

    [HttpGet(RouteCostants.GetIdentityRoute)]
    [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<IdentityDto>))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IdentityDto>> GetUserAsync(Guid id)
    {
        var identityModel = await identityService.GetAsync(id);

        return new IdentityDto
        {
            Id = identityModel.Id,
            FirstName = identityModel.FirstName,
            LastName = identityModel.LastName,
            Email = identityModel.Email,
            HashPassword = identityModel.HashPassword,
            Role = identityModel.Role,
        };
    }

    [HttpPut(RouteCostants.UpdateIdentityRoute)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IdentityDto>> UpdateUserAsync(IdentityDto updatedIdentityDto)
    {
        var identityModel = await identityService.UpdateAsync(new IdentityModel
        {
            Id = updatedIdentityDto.Id,
            Email = updatedIdentityDto.Email,
            FirstName = updatedIdentityDto.FirstName,
            LastName = updatedIdentityDto.LastName,
            HashPassword = updatedIdentityDto.HashPassword,
            Role = updatedIdentityDto.Role,
        });

        updatedIdentityDto.Role = identityModel.Role;
        updatedIdentityDto.Email = identityModel.Email;
        updatedIdentityDto.FirstName = identityModel.FirstName;
        updatedIdentityDto.LastName = identityModel.LastName;
        updatedIdentityDto.HashPassword = identityModel.HashPassword;
        updatedIdentityDto.Id = identityModel.Id;
        
        return updatedIdentityDto;
    }

    [HttpDelete(RouteCostants.DeleteIdentityRoute)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await identityService.DeleteAsync(id);
        return NoContent();
    }
}
