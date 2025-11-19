using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Autorization.API.Controllers;

[Route(RouteCostants.AuthRoute)]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost(RouteCostants.SignUpRoute)]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResultDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<AuthResultDto> SignUpAsync(SignUpDto signUpDto)
    {
        var result = await authService.SignUpAsync(new SignUpModel
        {
            Email = signUpDto.Email,
            Password = signUpDto.Password,
            ReEnteredPassword = signUpDto.ReEnteredPassword
        });

        var authResultDto = new AuthResultDto
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken
        };

        return authResultDto;
    }

    [HttpPost(RouteCostants.SignInRoute)]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResultDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<AuthResultDto> SignInAsync(SignInDto signInDto)
    {
        var result = await authService.SignInAsync(new SignInModel
        {
            Email = signInDto.Email,
            Password = signInDto.Password
        });

        var authResultDto = new AuthResultDto
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken
        };
        return authResultDto;
    }

    [HttpPost]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResultDto))]
    [ProducesResponseType(Status400BadRequest, Type = typeof(ErrorDto))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(ErrorDto))]
    public async Task<ActionResult> SignOutAsync()
    {
        var result = await authService.SignOutAsync();

        return Ok(result);
    }
}
