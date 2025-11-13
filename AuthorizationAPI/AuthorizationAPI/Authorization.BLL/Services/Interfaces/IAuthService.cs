using Aithorization.BLL.Models;

namespace Aithorization.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResultModel> SignInAsync(SignInModel signInModel, CancellationToken cancellationToken = default);
    Task<AuthResultModel> SignUpAsync(SignUpModel signUpModel, CancellationToken cancellationToken = default);
}
