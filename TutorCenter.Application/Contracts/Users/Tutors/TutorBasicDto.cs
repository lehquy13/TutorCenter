using TutorCenter.Application.Contracts.Courses.Dtos;
using TutorCenter.Application.Contracts.Models;

namespace TutorCenter.Application.Contracts.Users.Tutors;
public class TutorBasicDto : BasicAuditedEntityDto<int>
{
    //is tutor related informtions
    public string Role { get; set; } = "Tutor";
    public string AcademicLevel { get; set; } = "Student";
    public string University { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public short Rate { get; set; } = 5;
    public List<SubjectDto> Majors { get; set; } = new();
    public List<TutorVerificationInfoDto> TutorVerificationInfoDtos { get; set; } = new();

}