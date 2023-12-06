using TutorCenter.Application.Contracts.Models;
using TutorCenter.Domain.ClassInformationConsts;
using TutorCenter.Domain.Users;

namespace TutorCenter.Application.Contracts.Courses.Dtos;

public class RequestGettingClassDto : FullAuditedAggregateRootDto<int>
{
    public int TutorId { get; set; }
    public Tutor Tutor { get; set; } = null!;
    public int ClassInformationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}

public class RequestGettingClassForListDto : FullAuditedAggregateRootDto<int>
{
    public string Title { get; set; } = string.Empty;
    public int ClassInformationId { get; set; }
    public CourseDto ClassInformationDto { get; set; } = null!;
    public string SubjectName { get; set; } = string.Empty;
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}

public class RequestGettingClassMinimalDto : EntityDto<int>
{
    public int TutorId { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public string TutorPhoneNumber { get; set; } = string.Empty;
    public string TutorEmail { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}

public class RequestGettingClassExtendDto : RequestGettingClassDto
{
    public string LearnerName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
}