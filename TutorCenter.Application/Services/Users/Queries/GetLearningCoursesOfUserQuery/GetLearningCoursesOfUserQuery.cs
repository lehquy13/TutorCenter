using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Services.Users.Queries.GetLearningCoursesOfUserQuery;

public class GetLearningCoursesOfUserQuery : IRequest<List<CourseForListDto>>
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 100;
    public int ObjectId { get; set; } = 0;
    public GetLearningCoursesOfUserQuery()
    {
        PageIndex = 1;
    }
}