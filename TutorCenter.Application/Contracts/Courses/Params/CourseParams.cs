using TutorCenter.Application.Contracts.Models;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Contracts.Courses.Params;

public class CourseParams : PaginationParams
{
    public string Filter = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public Status? Status { get; set; }
}