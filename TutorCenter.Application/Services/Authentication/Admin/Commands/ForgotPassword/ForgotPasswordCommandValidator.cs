using TutorCenter.Application.Services.Authentication.Admin.Commands.ForgotPassword;
using FluentValidation;
namespace TutorCenter.Application.Services.Authentication.Commands.Register;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}

