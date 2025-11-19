using Authorization.BLL.Models;

namespace Authorization.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResultModel> SignInAsync(SignInModel signInModel, CancellationToken cancellationToken = default);
    Task<AuthResultModel> SignUpAsync(SignUpModel signUpModel, CancellationToken cancellationToken = default);
    Task<AuthResultModel> SignOutAsync(CancellationToken cancellationToken = default);
}
