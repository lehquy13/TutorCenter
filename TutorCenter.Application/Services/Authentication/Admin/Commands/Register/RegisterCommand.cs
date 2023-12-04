using TutorCenter.Contracts.Authentication;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Admin.Commands.Register;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ) : IRequest<AuthenticationResult>;

