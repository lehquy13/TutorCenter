using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetUsers;

public class GetUsersQuery : IRequest<Result<PaginatedList<UserDto>>>
{
    public GetUsersQuery()
    {
        PageIndex = 1;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 100;
}