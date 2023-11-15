using FluentResults;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;
/// <summary>
/// This command is used for admin to delete user
/// </summary>
/// <param name="UserId"></param>
public record DeleteUserCommand
(
    int UserId
    ) : IRequest<Result<bool>>;

