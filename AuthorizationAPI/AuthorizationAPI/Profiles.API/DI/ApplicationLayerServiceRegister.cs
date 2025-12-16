using FluentValidation;
using FluentValidation.AspNetCore;
using Profiles.API.Common.Validators.Doctors;
using Profiles.BLL.DI;
using System.Reflection;

namespace Profiles.API.DI;

public static class ApplicationLayerServiceRegister
{
    public static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton(TimeProvider.System);

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreatedDoctorDtoValidator>();

        services.RegisterBusinessLayerDependencies(configuration);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
