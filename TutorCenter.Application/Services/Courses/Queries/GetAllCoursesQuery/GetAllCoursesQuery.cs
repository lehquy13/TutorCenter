using TutorCenter.Application.Contracts;
using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Services.Abstractions.QueryHandlers;

namespace TutorCenter.Application.Services.Courses.Queries.GetAllCoursesQuery;

public class GetAllCoursesQuery : GetObjectQuery<PaginatedList<CourseForListDto>>
{
      public string SubjectName { get; set; } = string.Empty;
      public string? Status { get; set; }
  
      public string Filter = string.Empty;
      public GetAllCoursesQuery()
      {
          PageIndex = 1;
      }
    
}