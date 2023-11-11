using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ChangePassword;

public record CustomerChangePasswordCommand
(
    int Id,
    string CurrentPassword,
    string NewPassword,
    string ConfirmedPassword
    ) : IRequest<Result<AuthenticationResult>>;

