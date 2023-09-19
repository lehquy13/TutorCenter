using CED.Domain.Shared.ClassInformationConsts;
using MediatR;
using TutorCenter.Application.Contracts.Authentication;

namespace CED.Application.Services.Authentication.Customer.Commands.Register;

public record CustomerRegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    string Address,
    int BirthYear,
    Gender Gender
) : IRequest<AuthenticationResult>;