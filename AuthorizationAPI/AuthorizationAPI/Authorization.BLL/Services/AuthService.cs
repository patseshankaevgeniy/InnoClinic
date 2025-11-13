using Authorization.BLL.Models;
using Authorization.BLL.Services.Interfaces;
using Authorization.DAL.Entities;
using Authorization.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Authorization.BLL.Services;

public sealed class AuthService(IUserRepository userRepository) : IAuthService
{
    public async Task<AuthResultModel> SignInAsync(SignInModel signInModel, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(signInModel.Email))
        {
            throw new ValidationException("Email can't be null or empty");
        }

        if (string.IsNullOrEmpty(signInModel.Password))
        {
            throw new ValidationException("Password can't be null or empty");
        }

        var user = await userRepository.GetByEmailAsync(signInModel.Email, cancellationToken);
        if (user is null)
        {
            throw new NotImplementedException();
        }

        if (!string.Equals(user.Password, signInModel.Password))
        {
            throw new ValidationException("Invalid password");
        }

        return new AuthResultModel
        {
            AccessToken = "MockedAccess"
        };
    }

    public async Task<AuthResultModel> SignUpAsync(SignUpModel signUpModel, CancellationToken cancellationToken = default)
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

        if (!string.Equals(signUpModel.Password, signUpModel.ReEnteredPassword))
        {
            throw new ValidationException("Passwords do not match");
        }

        var user = await userRepository.GetByEmailAsync(signUpModel.Email, cancellationToken);
        if (user is not null)
        {
            throw new NotImplementedException();
        }

        var newUser = await userRepository.CreateAsync(new User
        {
            Id = Guid.NewGuid(),
            Email = signUpModel.Email,
            Password = signUpModel.Password,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }, cancellationToken);

        return new AuthResultModel
        {
            AccessToken = "MockedAccess"
        };
    }
}
