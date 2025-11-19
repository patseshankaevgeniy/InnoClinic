using Authorization.BLL.Constants;
using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Authorization.BLL.Services;

public sealed class AuthService(IIdentityRepository identityRepository,
    IJwtTokenService jwtTokenService) : IAuthService
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

        var user = await identityRepository.GetByEmailAsync(signInModel.Email, cancellationToken);
        if (user is null)
        {
            throw new ValidationException(ExceptionConstants.NoUser);
        }

        if (!string.Equals(user.HashPassword, signInModel.Password))
        {
            throw new ValidationException(ExceptionConstants.PasswordNotMatch);
        }

        return new AuthResultModel
        {
            AccessToken = jwtTokenService.GenerateToken(user),
            RefreshToken = jwtTokenService.GenerateRefreshToken()
        };
    }

    public Task<AuthResultModel> SignOutAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResultModel> SignUpAsync(SignUpModel signUpModel, CancellationToken cancellationToken = default)
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

        var user = await identityRepository.GetByEmailAsync(signUpModel.Email, cancellationToken);
        if (user is not null)
        {
            throw new ValidationException(ExceptionConstants.UserExists);
        }

        var newUser = await identityRepository.CreateAsync(new Identity
        {
            Id = Guid.NewGuid(),
            Email = signUpModel.Email,
            HashPassword = signUpModel.Password,
            FirstName = DefaultUserConstants.DefaultFirstName,
            LastName = DefaultUserConstants.DefaultLastName,
            Role = UserRole.Patient,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, cancellationToken);

        var authResultModel = await SignInAsync(new SignInModel
        {
            Email = newUser.Email,
            Password = newUser.HashPassword
        }, cancellationToken);

        return authResultModel;
    }
}
