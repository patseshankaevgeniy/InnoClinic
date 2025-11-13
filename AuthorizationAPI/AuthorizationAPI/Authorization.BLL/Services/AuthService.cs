using Aithorization.BLL.Models;
using Aithorization.BLL.Services.Interfaces;
using Aithorization.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Aithorization.BLL.Services;
public sealed class AuthService(IUserRepository _userRepository) : IAuthService
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

        var user = await _userRepository.GetByEmailAsync(signInModel.Email, cancellationToken);
        if (user == null)
        {
            throw new NotImplementedException();
        }

        if (user.Password != signInModel.Password)
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

        if (signUpModel.Password != signUpModel.ReEnteredPassword)
        {
            throw new ValidationException("Passwords do not match");
        }

        var user = await _userRepository.GetByEmailAsync(signUpModel.Email, cancellationToken);
        if (user != null)
        {
            throw new NotImplementedException();
        }

        var newUser = await _userRepository.CreateAsync(new DAL.Entities.User
        {
            Email = signUpModel.Email,
            Password = signUpModel.Password
        }, cancellationToken);

        

        return new AuthResultModel
        {
            AccessToken = "MockedAccess"
        };
    }
}
