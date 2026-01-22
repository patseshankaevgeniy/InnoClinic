using FluentValidation;
using Profiles.API.Dtos.Patients;

namespace Profiles.API.Common.Validators.Patients
{
    public class PatientDtoValidator : UserValidator<PatientDto>
    {
        public PatientDtoValidator(TimeProvider timeProvider) : base(timeProvider)
        {
        }
    }
}
