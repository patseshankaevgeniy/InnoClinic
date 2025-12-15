using FluentValidation;
using Profiles.BLL.Models.Doctors;

namespace Profiles.BLL.Common.Validators.Doctors;

public class CreatedDoctorModelValidator : UserValidator<CreatedDoctorModel>
{
    public CreatedDoctorModelValidator(TimeProvider timeProvider) : base(timeProvider)
    {
        DateTimeOffset now = timeProvider.GetUtcNow();

        RuleFor(x => x.Status)
            .NotNull();
        RuleFor(RuleFor => RuleFor.CareerStartAt)
            .NotNull()
            .LessThan(now.DateTime)
            .GreaterThan(now.AddYears(-100).DateTime)
            .GreaterThan(x => x.DateOfBirth.AddYears(18));
    }
}
