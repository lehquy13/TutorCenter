using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;

public class GetAllCoursesQuery : IRequest<Result<PaginatedList<CourseForListDto>>>
{
    public string Filter = string.Empty;

    public GetAllCoursesQuery()
    {
        PageIndex = 1;
    }

    public string SubjectName { get; set; } = string.Empty;
    public Status? Status { get; set; }

    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 100;
    public int ObjectId { get; set; } = 0;
}