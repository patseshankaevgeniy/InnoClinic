using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.API.Constants;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autorization.API.Controllers;

[Route(RouteCostants.AuthRoute)]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost(RouteCostants.SignUpRoute)]
    public async Task<ActionResult<AuthResultDto>> SignUpAsync(SignUpDto signUpDto)
    {
        var result = await authService.SignUpAsync(new SignUpModel
        {
            Email = signUpDto.Email,
            ReEnteredPassword = signUpDto.ReEnteredPassword,
            Password = signUpDto.Password
        });

        var authResultDto = new AuthResultDto
        {
            AccessToken = result.AccessToken
        };
        return Ok(authResultDto);
    }

    [HttpPost(RouteCostants.SignInRoute)]
    public async Task<ActionResult<AuthResultDto>> SignInAsync(SignInDto signInDto)
    {
        var result = await authService.SignInAsync(new SignInModel
        {
            Email = signInDto.Email,
            Password = signInDto.Password
        });

        var authResultDto = new AuthResultDto
        {
            AccessToken = result.AccessToken
        };
        return Ok(authResultDto);
    }
}
