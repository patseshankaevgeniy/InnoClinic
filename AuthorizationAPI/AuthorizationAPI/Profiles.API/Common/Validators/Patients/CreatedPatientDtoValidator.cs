using Profiles.API.Dtos.Patients;

namespace Profiles.API.Common.Validators.Patients
{
    public class CreatedPatientDtoValidator : UserValidator<CreatedPatientDto>
    {
        public CreatedPatientDtoValidator(TimeProvider timeProvider) : base(timeProvider)
        {
        }
    }
}
