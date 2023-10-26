using FluentResults;
using MediatR;
using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery;

public class GetAllCourseRequests : IRequest<Result<List<CourseRequestDto>>>
{
    public int ClassId { get; set; } = 0;
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 100;
    public int ObjectId { get; set; } = 0;
    GetAllCourseRequests() {
        PageIndex = 1;
    }
}