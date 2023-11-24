using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.Register;

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
) : IRequest<Result<AuthenticationResult>>;