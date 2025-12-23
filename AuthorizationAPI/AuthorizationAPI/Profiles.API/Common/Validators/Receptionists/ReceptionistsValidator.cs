using Profiles.API.Dtos.Receptionists;

namespace Profiles.API.Common.Validators.Receptionists;

public class ReceptionistsValidator : UserValidator<ReceptionistDto>
{
    public ReceptionistsValidator(TimeProvider timeProvider) : base(timeProvider)
    {
    }
}
