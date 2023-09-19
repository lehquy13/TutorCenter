using CED.Application.Services.Authentication.Admin.Commands.ChangePassword;
using FluentValidation;
using TutorCenter.Application.Services.Authentication.Admin.Commands.ChangePassword;

namespace CED.Application.Services.Authentication.Customer.Commands.ChangePassword;

public class CustomerChangePasswordCommandCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public CustomerChangePasswordCommandCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.ConfirmedPassword).NotEmpty();
    }
}

