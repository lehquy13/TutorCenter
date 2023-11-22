using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Users.Learners;

namespace TutorCenter.Application.Services.Users.Queries.Handlers.GetAllStudents;

public class GetAllStudentsQuery : IRequest<Result<PaginatedList<LearnerDto>>>
{
    public GetAllStudentsQuery()
    {
        PageIndex = 1;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 100;
}