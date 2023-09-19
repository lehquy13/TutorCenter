using TutorCenter.Application.Contracts.Models;
using TutorCenter.Domain.ClassInformationConsts;

namespace TutorCenter.Application.Contracts.Courses.Dtos;

public class CourseRequestDto : FullAuditedAggregateRootDto<int>
{
    public int TutorId { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public string TutorPhone { get; set; } = string.Empty;
    public string TutorEmail { get; set; }= string.Empty;
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
}
public class CourseRequestForDetailDto 
{
    
    public int Id { get; set; } = 0;
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
    public DateTime? LastModificationTime { get; set; } 
    
    public string TutorName { get; set; } = string.Empty;
    public string TutorPhone { get; set; } = string.Empty;
    public string TutorEmail { get; set; }= string.Empty;
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string RequestStatus { get; set; } = "Verifying"; 
    public string LearnerName { get; set; } = string.Empty;
                                                                                   public string LearnerContact { get; set; } = string.Empty;
}
public class CourseRequestForSuccessDetailDto 
{
    
    public int Id { get; set; } = 0;
   
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public RequestStatus RequestStatus { get; set; } = RequestStatus.Verifying;
   
}
public class CourseRequestForListDto : BasicAuditedEntityDto<int>
{
    public string Title { get; set; } = string.Empty;
    public int CourseId { get; set; }
    //public CourseDto CourseDto { get; set; } = null!;
    public string SubjectName { get; set; } = string.Empty;
    public string RequestStatus { get; set; } = "Verifying";
}

public class CourseRequestForCreateDto 
{
    public int CourseId { get; set; }
    public int TutorId { get; set; }
    
}