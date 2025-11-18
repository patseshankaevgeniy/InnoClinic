using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Authorization.API.Controllers;

[Route(RouteCostants.IdentityRoute)]
[ApiController]
[Authorize]
public class IdentityController(IIdenityService idenityService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route(RouteCostants.IdentityCreateRoute)]
    [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<IdentityDto>))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status403Forbidden, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IdentityDto>> CreateUserAsync(IdentityDto user)
    {
        var identityModel = await idenityService.CreateAsync(new IdentityModel
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Role = user.Role
        });

        user.Role = identityModel.Role;
        user.Email = identityModel.Email;
        user.FirstName = identityModel.FirstName;
        user.LastName = identityModel.LastName;
        user.Password = identityModel.Password;
        user.Id = identityModel.Id;

        return user;
    }

    [HttpGet(Name = RouteCostants.IdentityGetRoute)]
    [ProducesResponseType(Status200OK, Type = typeof(IEnumerable<IdentityDto>))]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IdentityDto>> GetUserAsync(string email)
    {
       var identityModel = await idenityService.GetAsync(email);

        return new IdentityDto
        {
            Id = identityModel.Id,
            FirstName = identityModel.FirstName,
            LastName = identityModel.LastName,
            Email = identityModel.Email,
            Password = identityModel.Password,
            Role = identityModel.Role,
            Status = identityModel.Status
        };
    }

    [HttpPut(Name = RouteCostants.IdentityUpdateRoute)]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status401Unauthorized, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status404NotFound, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult<IdentityDto>> UpdateUserAsync(IdentityDto user)
    {
        var identityModel = await idenityService.UpdateAsync(new IdentityModel
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Role = user.Role,
            Status = user.Status
        });

        user.Role = identityModel.Role;
        user.Email = identityModel.Email;
        user.FirstName = identityModel.FirstName;
        user.LastName = identityModel.LastName;
        user.Password = identityModel.Password;
        user.Id = identityModel.Id;
        user.Status = identityModel.Status;
        return user;
    }

}
