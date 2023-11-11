using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Authentications;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ForgotPassword;

public record CustomerForgotPasswordCommand
(
    string Email
    ) : IRequest<Result<AuthenticationResult>>;

