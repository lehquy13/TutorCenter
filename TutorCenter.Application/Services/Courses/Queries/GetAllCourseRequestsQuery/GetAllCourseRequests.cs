using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts.Courses.Dtos;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery;

public class GetAllCourseRequests : IRequest<Result<List<CourseRequestDto>>>
{
    private GetAllCourseRequests()
    {
        PageIndex = 1;
    }

    public int ClassId { get; set; } = 0;
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 100;
    public int ObjectId { get; set; } = 0;
}