using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetUserById;

public class GetUserByIdQuery : IRequest<Result<UserDto>>
{
    public int ObjectId { get; set; }
}