using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.BLL.Common.Validators.Doctors;
using Profiles.BLL.Models.Doctors;
using Profiles.BLL.Services;
using Profiles.BLL.Services.Interfaces;
using Profiles.DAL.DI;
using System.Reflection;

namespace Profiles.BLL.DI;

public static class BusinessLayerServiceRegister
{
    public static IServiceCollection RegisterBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDataLayerDependencies(configuration);

        // Register BLL services
        services.AddScoped<IDoctorsService, DoctorsService>();

        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register Validators
        services.AddScoped<IValidator<CreatedDoctorModel>, CreatedDoctorModelValidator>();
        services.AddScoped<IValidator<UpdatedDoctorModel>, UpdatedDoctorModelValidator>();

        return services;
    }
}
