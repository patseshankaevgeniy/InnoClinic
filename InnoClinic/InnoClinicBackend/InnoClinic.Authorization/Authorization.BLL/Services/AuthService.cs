using Authorization.BLL.Constants;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.Common.Interfaces;
using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Authorization.BLL.Services;

public sealed class AuthService(
    IIdentityRepository identityRepository,
    IJwtTokenService jwtTokenService,
    IPasswordHasher passwordHasher) : IAuthService
{
    public async Task<AuthResultModel> SignInAsync(SignInModel signInModel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(signInModel.Email))
        {
            throw new ValidationException(ExceptionConstants.WrongEmail);
        }

        if (string.IsNullOrEmpty(signInModel.Password))
        {
            throw new ValidationException(ExceptionConstants.WrongPassword);
        }

        var identity = await identityRepository.GetByEmailAsync(signInModel.Email, cancellationToken);
        if (identity is null)
        {
            throw new ValidationException(ExceptionConstants.NoUser);
        }

        if (!passwordHasher.VerifyPassword(signInModel.Password, identity.HashPassword))
        {
            throw new ValidationException(ExceptionConstants.PasswordNotMatch);
        }

        return new AuthResultModel
        {
            AccessToken = jwtTokenService.GenerateToken(identity),
            RefreshToken = jwtTokenService.GenerateRefreshToken()
        };
    }

    public async Task<AuthResultModel> SignUpAsPatientAsync(SignUpModel signUpModel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(signUpModel.ReEnteredPassword))
        {
            throw new ValidationException(ExceptionConstants.WrongPassword);
        }

        if (string.IsNullOrEmpty(signUpModel.Password))
        {
            throw new ValidationException(ExceptionConstants.WrongPassword);
        }

        if (string.IsNullOrEmpty(signUpModel.Email))
        {
            throw new ValidationException(ExceptionConstants.WrongEmail);
        }

        if (!string.Equals(signUpModel.Password, signUpModel.ReEnteredPassword))
        {
            throw new ValidationException(ExceptionConstants.PasswordNotMatch);
        }

        var identity = await identityRepository.GetByEmailAsync(signUpModel.Email, cancellationToken);
        if (identity is not null)
        {
            throw new ValidationException(ExceptionConstants.UserExists);
        }

        var newIdentity = await identityRepository.CreateAsync(new Identity
        {
            Email = signUpModel.Email,
            HashPassword = passwordHasher.HashPassword(signUpModel.Password),
            Role = UserRole.Patient,
        }, cancellationToken);

        var authResultModel = await SignInAsync(new SignInModel
        {
            Email = newIdentity.Email,
            Password = newIdentity.HashPassword
        }, cancellationToken);

        return authResultModel;
    }
}
