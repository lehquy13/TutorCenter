using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Admin.Commands.ChangePassword;

public record ChangePasswordCommand
(
    int Id,
    string CurrentPassword,
    string NewPassword,
    string ConfirmedPassword
    ) : IRequest<AuthenticationResult>;

