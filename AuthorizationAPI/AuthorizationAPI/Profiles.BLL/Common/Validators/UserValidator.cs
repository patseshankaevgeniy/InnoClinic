using FluentValidation;
using Profiles.BLL.Models;

namespace Profiles.BLL.Common.Validators;

public abstract class UserValidator<TModel> : AbstractValidator<TModel> where TModel : UserModel
{
    protected DateTimeOffset Now { get; }

    protected UserValidator(TimeProvider timeProvider)
    {
        this.Now = timeProvider.GetUtcNow();

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
            .LessThan(Now.DateTime)
            .GreaterThan(Now.AddYears(-150).DateTime);
    }
}
