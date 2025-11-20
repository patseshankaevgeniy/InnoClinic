using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Autorization.API.Controllers;

[ApiController]
[Route(RouteCostants.AuthControllerRoute)]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost(RouteCostants.SignUpRoute)]
    [ProducesResponseType(typeof(AuthResultDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError )]
    public async Task<AuthResultDto> SignUpAsync(SignUpDto signUpDto)
    {
        var result = await authService.SignUpAsync(new SignUpModel
        {
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName,
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
    [ProducesResponseType(typeof(AuthResultDto), Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), Status500InternalServerError)]
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
}
