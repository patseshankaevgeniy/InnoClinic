using Authorization.Application.Models;
using Authorization.Application.Models.Auth;
using Authorization.Application.Services.Interfaces;
using Authorization.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _tokenService;

    public AuthService(
            IUserRepository userRepository,
            IJwtTokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<SignInResultModel> SignInAsync(SignInModel signInDto)
    {
        if (string.IsNullOrEmpty(signInDto.Email))
        {
            throw new ValidationException("Email can't be null or empty");
        }

        if (string.IsNullOrEmpty(signInDto.Password))
        {
            throw new ValidationException("Password can't be null or empty");
        }

        var user = await _userRepository.FindByEmailAsync(signInDto.Email);
        if (user == null)
        {
            return new SignInResultModel
            {
                Succeeded = false,
                ErrorType = (int)AuthErrorType.UserNotFound
            };
        }

        if (user.Password != signInDto.Password)
        {
            return new SignInResultModel
            {
                Succeeded = false,
                ErrorType = (int)AuthErrorType.WrongPassword
            };
        }

        return new SignInResultModel
        {
            Succeeded = true,
            Token = _tokenService.GenerateToken(new() { UserId = user.Id })
        };
    }

    public async Task<SignUpResultModel> SignUpAsync(SignUpModel signUpModel)
    {
        if (string.IsNullOrEmpty(signUpModel.ReEnteredPassword))
        {
            throw new ValidationException("Password can't be null or empty");
        }

        if (string.IsNullOrEmpty(signUpModel.Password))
        {
            throw new ValidationException("Password can't be null or empty");
        }

        if (string.IsNullOrEmpty(signUpModel.Email))
        {
            throw new ValidationException("Email can't be null or empty");
        }

        if (signUpModel.Password != signUpModel.ReEnteredPassword)
        {
            throw new ValidationException("Passwords do not match");
        }

        var user = await _userRepository.FindByEmailAsync(signUpModel.Email);
        if (user != null)
        {
            return new SignUpResultModel
            {
                Succeeded = false,
                ErrorType = (int)AuthErrorType.LoginAlreadyExists
            };
        }

        user = new User
        {
            Email = signUpModel.Email,
            Password = signUpModel.Password,
        };

        await _userRepository.CreateAsync(user);
        return new SignUpResultModel { Succeeded = true };
    }
}
