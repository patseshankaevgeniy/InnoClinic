using Aithorization.API.Constants;
using Aithorization.API.Dtos;
using Aithorization.BLL.Models;
using Aithorization.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autorization.API.Controllers;

[Route(RouteCostants.AuthRoute)]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost(RouteCostants.SignUpRoute)]
    public async Task<ActionResult<AuthResultDto>> SignUpAsync(SignUpDto signUpDto)
    {
        var result = await _authService.SignUpAsync(new SignUpModel
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
        var result = await _authService.SignInAsync(new SignInModel
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