using Authorization.Application.Models.Auth;

namespace Authorization.Application.Services.Interfaces;

public interface IAuthService
{
    Task<SignInResultModel> SignInAsync(SignInModel signInDto);
    Task<SignInResultModel> SignUpAsync(SignInModel signUpDto);
}
