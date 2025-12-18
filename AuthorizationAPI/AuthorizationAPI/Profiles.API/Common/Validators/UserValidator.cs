using FluentValidation;
using Profiles.API.Dtos;

namespace Profiles.API.Common.Validators;

public abstract class UserValidator<TModel> : AbstractValidator<TModel> where TModel : UserDto
{
    private readonly TimeProvider timeProvider;


    protected UserValidator(TimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(20);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(20);
        RuleFor(x => x.MiddleName)
            .MaximumLength(20);
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(20)
            .Matches(@"^\+?[\d\s\-]{8,20}$");
        RuleFor(x => x.DateOfBirth)
            .NotNull()
            .LessThan(timeProvider.GetUtcNow().DateTime)
            .GreaterThan(timeProvider.GetUtcNow().AddYears(-150).DateTime);
        
    }
}
