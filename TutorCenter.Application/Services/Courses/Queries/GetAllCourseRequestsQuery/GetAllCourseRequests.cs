using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCourseRequestsQuery;

public class GetAllCourseRequests : GetObjectQuery<List<CourseRequestForListDto>>
{
    public int ClassId { get; set; } = 0;
}