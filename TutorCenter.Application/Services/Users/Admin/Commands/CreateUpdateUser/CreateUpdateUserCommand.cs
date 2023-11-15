using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users;

namespace CED.Application.Services.Users.Admin.Commands;

public record CreateUpdateUserCommand
    (
     UserDto UserDto ,
         string FilePath
): IRequest<Result<bool>>;

