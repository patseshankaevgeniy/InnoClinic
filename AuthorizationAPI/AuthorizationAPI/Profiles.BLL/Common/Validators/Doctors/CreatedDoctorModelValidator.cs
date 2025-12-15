using FluentValidation;
using Profiles.BLL.Models.Doctors;

namespace Profiles.BLL.Common.Validators.Doctors;

public class CreatedDoctorModelValidator : UserValidator<CreatedDoctorModel>
{
    public CreatedDoctorModelValidator(TimeProvider timeProvider) : base(timeProvider)
    {
        RuleFor(x => x.Status)
            .NotNull();
        RuleFor(RuleFor => RuleFor.CareerStartAt)
            .NotNull()
            .LessThan(this.Now.DateTime)
            .GreaterThan(this.Now.AddYears(-100).DateTime)
            .GreaterThan(x => x.DateOfBirth.AddYears(18));
    }
}
