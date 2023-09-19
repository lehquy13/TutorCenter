using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Common.Models;
using TutorCenter.Domain.Users;

namespace TutorCenter.Domain.Courses;
public class CourseRequest : AuditedEntity<int>
{
    public int TutorId { get; set; }
    public Tutor Tutor { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}