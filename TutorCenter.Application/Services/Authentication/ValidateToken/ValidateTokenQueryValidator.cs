using FluentValidation;

namespace TutorCenter.Application.Services.Authentication.ValidateToken;

public class ValidateTokenQueryValidator : AbstractValidator<ValidateTokenQuery>
{
    public ValidateTokenQueryValidator()
    {
        RuleFor(x => x.ValidateToken).NotEmpty();
    }
}