using FluentValidation;

namespace TutorCenter.Application.Services.Authentication.Admin.Commands.ChangePassword;

public class ChangePasswordCommandCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.ConfirmedPassword).NotEmpty();
    }
}

