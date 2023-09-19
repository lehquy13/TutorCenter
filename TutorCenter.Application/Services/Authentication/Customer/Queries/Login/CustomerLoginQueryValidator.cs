using FluentValidation;

namespace CED.Application.Services.Authentication.Customer.Queries.Login;

public class CustomerLoginQueryValidator : AbstractValidator<CustomerLoginQuery>
{
    public CustomerLoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

