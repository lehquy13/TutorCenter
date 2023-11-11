using FluentResults;
using MediatR;

namespace TutorCenter.Application.Services.Authentication.Customer.Commands.ChangePassword;

public record ChangePasswordWhenForgotCommand
(
    int Id,
    string NewPassword
) : IRequest<Result<bool>>;

