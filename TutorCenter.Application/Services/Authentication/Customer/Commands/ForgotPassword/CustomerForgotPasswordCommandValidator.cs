using FluentValidation;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public class CustomerForgotPasswordCommandValidator : AbstractValidator<CustomerForgotPasswordCommand>
{
    public CustomerForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}