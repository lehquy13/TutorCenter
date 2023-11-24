using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Services.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<Result<UserForDetailDto>>
{
    public int Id { get; set; }
}
