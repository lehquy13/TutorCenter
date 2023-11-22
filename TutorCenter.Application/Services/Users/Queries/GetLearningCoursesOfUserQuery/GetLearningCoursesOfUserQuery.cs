using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Users.Queries.GetLearningCoursesOfUserQuery;

public class GetLearningCoursesOfUserQuery : IRequest<Result<PaginatedList<CourseForListDto>>>
{
    public GetLearningCoursesOfUserQuery()
    {
        PageIndex = 1;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 100;
    public int ObjectId { get; set; } = 0;
}