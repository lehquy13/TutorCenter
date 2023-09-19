using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Courses.Queries.GetLearningCourses;

public class GetLearningClassesOfUserQuery : GetObjectQuery<PaginatedList<CourseForListDto>>
{
    
}