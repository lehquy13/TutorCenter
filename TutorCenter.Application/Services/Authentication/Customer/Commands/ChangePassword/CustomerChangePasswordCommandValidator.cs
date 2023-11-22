using FluentValidation;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ChangePassword;

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