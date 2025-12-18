using Profiles.API.Dtos.Patients;

namespace Profiles.API.Common.Validators.Patients
{
    public class UpdatedPatientDtoValidator : UserValidator<UpdatedPatientDto>
    {
        public UpdatedPatientDtoValidator(TimeProvider timeProvider) : base(timeProvider)
        {
        }
    }
}
