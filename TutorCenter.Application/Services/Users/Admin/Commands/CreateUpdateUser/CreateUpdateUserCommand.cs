using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Services.Users.Admin.Commands.CreateUpdateUser;

public record CreateUpdateUserCommand
(
    UserForDetailDto UserDto,
    string FilePath
) : IRequest<Result<bool>>;