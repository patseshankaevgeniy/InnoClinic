using Authorization.API.Constants;
using Authorization.API.Dtos;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autorization.API.Controllers;

[Authorize]
[Route(RouteCostants.AuthRoute)]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost(RouteCostants.SignUpRoute)]
    public async Task<AuthResultDto> SignUpAsync(SignUpDto signUpDto)
    {
        var result = await authService.SignUpAsync(new SignUpModel
        {
            Email = signUpDto.Email,
            ReEnteredPassword = signUpDto.ReEnteredPassword,
            Password = signUpDto.Password
        });

        var authResultDto = new AuthResultDto
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken
        };

        return authResultDto;
    }

    [AllowAnonymous]
    [HttpPost(RouteCostants.SignInRoute)]
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
