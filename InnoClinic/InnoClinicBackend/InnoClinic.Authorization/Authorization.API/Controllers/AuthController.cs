using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Autorization.API.Controllers;

[ApiController]
[Route(RouteCostants.AuthControllerRoute)]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost(RouteCostants.SignUpRoute)]
    [ProducesResponseType(typeof(AuthResultDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError )]
    public async Task<AuthResultDto> SignUpAsync(SignUpDto signUpDto)
    {
        var result = await authService.SignUpAsPatientAsync(mapper.Map<SignUpModel>(signUpDto));

        return mapper.Map<AuthResultDto>(result);
    }

    [HttpPost(RouteCostants.SignInRoute)]
    [ProducesResponseType(typeof(AuthResultDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
    public async Task<AuthResultDto> SignInAsync(SignInDto signInDto)
    {
        var result = await authService.SignInAsync(mapper.Map<SignInModel>(signInDto));

        return mapper.Map<AuthResultDto>(result);
    }
}
