using MediatR;
using TutorCenter.Application.Contracts.Authentication;

namespace CED.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public record CustomerForgotPasswordCommand
(
    string Email
    ) : IRequest<AuthenticationResult>;

