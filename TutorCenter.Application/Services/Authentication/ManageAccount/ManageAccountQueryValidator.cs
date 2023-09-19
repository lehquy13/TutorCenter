using FluentValidation;

namespace CED.Application.Services.Authentication.ManageAccount;

public class ManageAccountQueryValidator : AbstractValidator<ManageAccountQuery>
{
    public ManageAccountQueryValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
       
    }
}

