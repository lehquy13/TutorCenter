using MediatR;

namespace TutorCenter.Application.Contracts.Authentications;

public record ChangePasswordCommand
(
    int Id,
    string CurrentPassword,
    string NewPassword,
    string ConfirmedPassword
):IRequest<AuthenticationResult>;