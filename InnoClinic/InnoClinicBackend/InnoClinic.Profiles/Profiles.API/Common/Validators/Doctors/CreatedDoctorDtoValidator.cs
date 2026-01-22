using FluentValidation;
using Profiles.API.Common.Validators;
using Profiles.API.Dtos.Doctors;

namespace Profiles.API.Common.Validators.Doctors;

public class CreatedDoctorDtoValidator : UserValidator<CreatedDoctorDto>
{
    public CreatedDoctorDtoValidator(TimeProvider timeProvider) : base(timeProvider)
    {
        RuleFor(x => x.Status)
            .NotNull();
        RuleFor(RuleFor => RuleFor.CareerStartAt)
            .NotNull()
            .LessThan(timeProvider.GetUtcNow().DateTime)
            .GreaterThan(timeProvider.GetUtcNow().AddYears(-100).DateTime)
            .GreaterThan(x => x.DateOfBirth.AddYears(18));
    }
}
