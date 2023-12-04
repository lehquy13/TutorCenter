using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Admin.Commands.ForgotPassword;

public record ForgotPasswordCommand
(
    string Email
    ) : IRequest<AuthenticationResult>;

